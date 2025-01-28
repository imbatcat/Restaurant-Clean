using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Applications.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantCommandHandler(
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{UserName} [{UserId}] Creating new restaurant {@Restaurant}", currentUser.Email, currentUser.Id, request);
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;
            int id = await unitOfWork.restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}