using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Applications.Restaurants.Commands.CreateRestaurants;
using Restaurants.Applications.Restaurants.Commands.DeleteRestaurants;
using Restaurants.Applications.Restaurants.Commands.PatchRestaurants;
using Restaurants.Applications.Restaurants.Dtos;
using Restaurants.Applications.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Applications.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = PolicyNames.OwnsAtLeastTwoRestaurants)]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery { });
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.AtLeast20)]
        public async Task<ActionResult<RestaurantDto>> GetOne([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantsByIdQuery(id));

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {

            int id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetOne), new { id }, null);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchRestaurant([FromRoute] int id, [FromBody] PatchRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));

            return NoContent();
        }
    }
}
