using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Applications.Ultilities.Identity.Authentication;
using Restaurants.Applications.Ultilities.Identity.Data;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/identities")]
    public class IdentityController(IServiceProvider sp) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();

            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException($"{nameof(Register)} requires a user store with email support.");
            }
            var userStore = sp.GetRequiredService<IUserStore<User>>();
            var emailStore = (IUserEmailStore<User>)userStore;
            var email = registerRequest.Email;
            var username = registerRequest.Username;

            var user = new User();
            await userStore.SetUserNameAsync(user, username, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);

            var result = await userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            await userManager.AddToRoleAsync(user, UserRoles.User);
            //TODO: Send confirmation email here

            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            var userStore = sp.GetRequiredService<IUserStore<User>>();
            var passwordStore = sp.GetRequiredService<IPasswordHasher<User>>();
            var idTokenProvider = sp.GetRequiredService<IdTokenProvider>();
            var accessTokenProvider = sp.GetRequiredService<AccessTokenProvider>();

            var signInManager = sp.GetRequiredService<SignInManager<User>>();
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            //TODO: Add !user.EmailVerified)
            if (user is null)
            {
                return BadRequest("User was not found");
            }

            var passwordVerificationResult = passwordStore.VerifyHashedPassword(user, user.PasswordHash!, loginRequest.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return BadRequest("Incorrect password");
            }

            //TODO: maybe add 'remember me' option
            var result = await signInManager.PasswordSignInAsync(
                user,
                loginRequest.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded) //result may not succeed due to invalid 2FA code, not just incorrect password.
            {
                return Unauthorized("Your authentication attempt failed, please try again with valid credentials");
            }
            var userRoles = await userManager.GetRolesAsync(user);
            var response = new AuthenticationResponse
            {
                AccessToken = accessTokenProvider.CreateToken(user, userRoles.First()),
                IdToken = idTokenProvider.CreateToken(user)
            };

            return Ok(response);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> Refresh([FromBody] LoginRequest loginRequest)
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            var userStore = sp.GetRequiredService<IUserStore<User>>();
            var passwordStore = sp.GetRequiredService<PasswordHasher<User>>();
            var tokenProvider = sp.GetRequiredService<IdTokenProvider>();

            var signInManager = sp.GetRequiredService<SignInManager<User>>();
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            //TODO: Add !user.EmailVerified)
            if (user is null)
            {
                throw new BadHttpRequestException("User was not found");
            }

            var passwordVerificationResult = passwordStore.VerifyHashedPassword(user, user.PasswordHash!, loginRequest.Password);

            if (passwordVerificationResult.HasFlag(PasswordVerificationResult.Failed))
            {
                return BadRequest("Incorrect password");
            }

            //TODO: maybe add 'remember me' option
            var result = await signInManager.PasswordSignInAsync(
                user,
                loginRequest.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (!result.Succeeded) //result may not succeed due to invalid 2FA code, not just incorrect password.
            {
                return Unauthorized("Your authentication attempt failed, please try again with valid credentials");
            }
            string token = tokenProvider.CreateToken(user);

            return Ok(token);
        }
    }
}