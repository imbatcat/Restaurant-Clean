using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Dishes.Commands.DeleteDishes
{
    public class DeleteAllDishesByRestaurantIdCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
