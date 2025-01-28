using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Domain.Authorization.Requirements
{
    public class MinimumRestaurantOwnership(int minimumRestaurants) : IAuthorizationRequirement
    {
        public int MinimumRestaurants { get; } = minimumRestaurants;
    }
}
