using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Users;
using Restaurants.Domain.Authorization.Requirements;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    internal class MinimumAgeRequirementHandler(
        ILogger<MinimumAgeRequirementHandler> logger,
        IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser() ?? throw new UnauthorizedAccessException();

            logger.LogInformation("User: {Email}, date of birth {DoB} - Handling MinimumAgeRequirement",
                currentUser.Email,
                currentUser.DateOfBirth);

            if (currentUser.DateOfBirth == null)
            {
                logger.LogWarning("User date of birth is null");
                context.Fail();
            }
            else if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                logger.LogWarning("Authorization failed, user does not meet the age requirements");
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}