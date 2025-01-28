using Microsoft.Extensions.Logging;
using Restaurants.Applications.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Services;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(
        ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();

            //user is guaranteed to exist, unless the request does not have the authorization token
            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
                user!.Email,
                resourceOperation,
                restaurant.Name);

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/Read operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Restaurant owner - successful authorization");
                return true;
            }

            return false;
        }
    }
}