using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Restaurants.Commands.DeleteRestaurants
{
    public class DeleteRestaurantCommand(int id): IRequest
    {
        public int Id { get; } = id;
    }
}
