using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Applications.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantCommandHandler(
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new restaurant {@Restaurant}", request);
            var restaurant = mapper.Map<Restaurant>(request);
            int id = await restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}
