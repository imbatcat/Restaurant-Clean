using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Users;
using Restaurants.Domain.Authorization.Requirements;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinimumRestaurantOwnershipHandler(
            ILogger<MinimumRestaurantOwnershipHandler> logger,
            IUserContext userContext,
            IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<MinimumRestaurantOwnership>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantOwnership requirement)
        {
            var currentUser = userContext.GetCurrentUser() ?? throw new UnauthorizedAccessException();
            var userRestaurants = await restaurantsRepository.GetAllAsync(p => p.OwnerId == currentUser!.Id);
            var restaurantCount = userRestaurants.Count();

            logger.LogInformation("User: {Email}, owner of {RestaurantCount} restaurants - Handling MinimumRestaurantOwnership",
                currentUser.Email,
                restaurantCount);

            if (restaurantCount < requirement.MinimumRestaurants)
            {
                context.Fail();
            }
            else
            {
                logger.LogInformation("Authorization success");
                context.Succeed(requirement);
            }
        }
    }
}