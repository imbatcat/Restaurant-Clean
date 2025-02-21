using Restaurants.Domain.Entities;

namespace Restaurants.Applications.Specifications.Composite
{
    public class OwnerHasAtLeastThreeRestaurantsWithAtMostFiveDishesSpecification : Specification<User>
    {
        public OwnerHasAtLeastThreeRestaurantsWithAtMostFiveDishesSpecification() :
            base(owner => owner.OwnedRestaurants.Count >= 3 &&
                        owner.OwnedRestaurants.All(r => r.Dishes.Count > 2))
        {
        }
    }
}