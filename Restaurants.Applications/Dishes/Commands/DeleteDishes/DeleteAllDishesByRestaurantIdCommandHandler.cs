using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Dishes.Commands.DeleteDishes
{
    public class DeleteAllDishesByRestaurantIdCommandHandler(
        ILogger<DeleteAllDishesByRestaurantIdCommandHandler> logger,
        IDishesRepository dishesRepository) : IRequestHandler<DeleteAllDishesByRestaurantIdCommand>
    {
        public async Task Handle(DeleteAllDishesByRestaurantIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting all dishes by restaurant with id {RestaurantId}", request.RestaurantId);
            await dishesRepository.DeleteAllAsync(p => p.RestaurantId == request.RestaurantId);
        }
    }
}
