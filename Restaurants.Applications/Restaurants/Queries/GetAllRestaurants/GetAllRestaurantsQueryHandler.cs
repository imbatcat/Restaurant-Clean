using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Common;
using Restaurants.Applications.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using System.Diagnostics;

namespace Restaurants.Applications.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(
        ILogger<GetAllRestaurantsQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
    {
        public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants, totalCount) = await unitOfWork.restaurantsRepository.GetAllMatchingAsync(request.searchPhrase, 
                request.PageNumber,
                request.PageSize,
                request.SortBy,
                request.SortDirection);

            var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            var result = new PagedResult<RestaurantDto>(restaurantDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }
}
