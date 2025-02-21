using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System.Security.Claims;
using System.Text;

namespace Restaurants.Applications.Ultilities.Identity.Authentication
{
    public sealed class IdTokenProvider(IConfiguration configuration) 
    {
        public string CreateToken(User user)
        {
            var jwtSettings = configuration.GetSection("JwtIDTokenSettings");
            string secretKey = jwtSettings["Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtIDTokenSettings:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = jwtSettings["Issuer"]!,
                Audience = jwtSettings["Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptior);

            return token;
        }
    }
}