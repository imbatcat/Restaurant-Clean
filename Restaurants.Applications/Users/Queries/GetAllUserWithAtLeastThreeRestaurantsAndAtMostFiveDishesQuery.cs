using MediatR;
using Restaurants.Applications.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Users.Queries
{
    public class GetAllUserWithAtLeastThreeRestaurantsAndAtMostFiveDishesQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
