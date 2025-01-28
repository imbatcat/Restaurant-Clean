using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Services;
using Restaurants.Infrastructure.Authorization;


namespace Restaurants.Applications.Dishes.Commands.CreateDishes
{
    public class CreateDishCommandHandler(
        ILogger<CreateDishCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService
        ) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish: {@DishRequest}", request);
            var restaurant = await restaurantsRepository.GetOneAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = mapper.Map<Dish>(request);

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create)) throw new ForbidenException();
            var dishId = await dishesRepository.Create(dish);

            return dishId;
        }
    }
}
