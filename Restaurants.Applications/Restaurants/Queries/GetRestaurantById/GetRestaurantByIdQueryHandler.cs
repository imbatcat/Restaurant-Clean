using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Applications.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(
        ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository
        ) : IRequestHandler<GetRestaurantsByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantsByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting restaurant by {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetOneAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }
    }
}
