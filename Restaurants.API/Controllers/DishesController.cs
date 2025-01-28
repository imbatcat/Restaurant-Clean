using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Applications.Dishes.Commands.CreateDishes;
using Restaurants.Applications.Dishes.Commands.DeleteDishes;
using Restaurants.Applications.Dishes.Queries.GetAllDishes;
using Restaurants.Applications.Dishes.Queries.GetDishByIdByRestaurantId;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurant/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDishByIdByRestaurantId), new { restaurantId, dishId }, null);
        }

        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetDishByIdByRestaurantId([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdByRestaurantIdQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.AtLeast20)]
        public async Task<IActionResult> GetAllDishesByRestaurantId([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetAllDishesQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllDishByRestaurantId([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteAllDishesByRestaurantIdCommand(restaurantId));
            return NoContent();
        }
    }
}