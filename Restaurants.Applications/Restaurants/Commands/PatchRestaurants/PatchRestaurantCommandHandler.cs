using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Applications.Restaurants.Commands.PatchRestaurants
{
    public class PatchRestaurantCommandHandler(
        ILogger<PatchRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository
        ) : IRequestHandler<PatchRestaurantCommand>
    {
        public async Task Handle(PatchRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Patching restaurant with id: {request.Id}");
            var restaurant = await restaurantsRepository.GetOneAsync(request.Id);

            if ( restaurant != null )
            {
                mapper.Map(request, restaurant);
                //restaurant.Name = request.Name;
                //restaurant.Description = request.Description;
                //restaurant.HasDelivery = request.HasDelivery;

                await restaurantsRepository.PatchAsync(restaurant);
            }
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }
    }
}
