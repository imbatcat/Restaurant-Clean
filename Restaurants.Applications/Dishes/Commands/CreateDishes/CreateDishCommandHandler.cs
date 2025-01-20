using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;


namespace Restaurants.Applications.Dishes.Commands.CreateDishes
{
    public class CreateDishCommandHandler(
        ILogger<CreateDishCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository
        ) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish: {@DishRequest}", request);
            var restaurant = await restaurantsRepository.GetOneAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = mapper.Map<Dish>(request);

            var dishId = await dishesRepository.Create(dish);

            return dishId;
        }
    }
}
