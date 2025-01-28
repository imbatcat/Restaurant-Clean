using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Services;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.Applications.Restaurants.Commands.DeleteRestaurants
{
    public class DeleteRestaurantCommandHandler(
        ILogger<DeleteRestaurantCommand> logger,
        IUnitOfWork unitOfWork,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with {RestaurantId}", request.Id);
            var restaurant = await unitOfWork.restaurantsRepository.GetOneAsync(request.Id);

            if (restaurant != null && !restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            {
                await unitOfWork.restaurantsRepository.DeleteAsync(restaurant);
            }
            else throw new ForbidenException();

                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }
    }
}