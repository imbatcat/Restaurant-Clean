using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Services;
using Restaurants.Infrastructure.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Dishes.Commands.DeleteDishes
{
    public class DeleteAllDishesByRestaurantIdCommandHandler(
        ILogger<DeleteAllDishesByRestaurantIdCommandHandler> logger,
        IDishesRepository dishesRepository,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteAllDishesByRestaurantIdCommand>
    {
        public async Task Handle(DeleteAllDishesByRestaurantIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting all dishes by restaurant with id {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetOneAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete)) throw new ForbidenException();
            await dishesRepository.DeleteAllAsync(p => p.RestaurantId == request.RestaurantId);
        }
    }
}
