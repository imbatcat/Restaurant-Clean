using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Dishes.Queries.GetDishByIdByRestaurantId
{
    public class GetDishByIdByRestaurantIdQueryHandler(
        ILogger<GetDishByIdByRestaurantIdQueryHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetDishByIdByRestaurantIdQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get dish with id {DishId} in restaurant with id {RestaurantId}", request.dishId, request.restaurantId);
            var restaurant = await restaurantsRepository.GetOneAsync(request.restaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.restaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(p => p.Id == request.dishId) ?? throw new NotFoundException(nameof(Dish), request.dishId.ToString());
            
            var dishDto = mapper.Map<DishDto>(dish);
            return dishDto;
        }
    }
}
