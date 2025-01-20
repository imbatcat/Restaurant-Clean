
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Applications.Restaurants.Commands.DeleteRestaurants
{
    public class DeleteRestaurantCommandHandler(
        ILogger<DeleteRestaurantCommand> logger,
        IRestaurantsRepository repository
        ) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with {RestaurantId}", request.Id);
            var restaurant = await repository.GetOneAsync(request.Id);

            if (restaurant != null)
            {
                await repository.DeleteAsync(restaurant);
            }
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }
    }
}
