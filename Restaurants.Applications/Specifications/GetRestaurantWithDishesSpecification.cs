using Restaurants.Domain.Entities;

namespace Restaurants.Applications.Specifications
{
    public class GetRestaurantWithDishesSpecification : Specification<Restaurant>
    {
        public GetRestaurantWithDishesSpecification(int id) : base(restaurant => restaurant.Id == id)
        {
            AddInclude(restaurant => restaurant.Dishes);
        }
    }
}