using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Dishes.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Applications.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQueryHandler(
        ILogger<GetAllDishesQueryHandler> logger,
        IMapper mapper,
        IDishesRepository dishesRepository) : IRequestHandler<GetAllDishesQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Displaying all dishes for restaurant {RestaurantId}", request.RestaurantId);
            var restaurant = await dishesRepository.GetAllAsync(p => p.RestaurantId == request.RestaurantId);

            var restaurantDto = mapper.Map<IEnumerable<DishDto>>(restaurant);
            return restaurantDto!;
        }
    }
}
