using Restaurants.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurants.Domain.Specifications
{
    public class GetRestaurantWithDishesSpecification : Specification<Restaurant>
    {
        public GetRestaurantWithDishesSpecification(int id) : base(restaurant => restaurant.Id == id)
        {
            AddInclude(restaurant => restaurant.Dishes);
        }
    }
}