using MediatR;
using Restaurants.Applications.Dishes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Dishes.Queries.GetDishByIdByRestaurantId
{
    public class GetDishByIdByRestaurantIdQuery(int restaurantId, int dishId) : IRequest<DishDto>
    {
        public int restaurantId { get; } = restaurantId;
        public int dishId { get; } = dishId;
    }
}
