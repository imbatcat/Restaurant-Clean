using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Applications.Users.Dtos;
using Restaurants.Applications.Users.Queries;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[Authorize]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserWithAtLeastThreeRestaurantsWithAtMostFiveDishesSpecification()
        {
            var result = await mediator.Send(new GetAllUserWithAtLeastThreeRestaurantsAndAtMostFiveDishesQuery());

            return Ok(result);
        }
    }
}
