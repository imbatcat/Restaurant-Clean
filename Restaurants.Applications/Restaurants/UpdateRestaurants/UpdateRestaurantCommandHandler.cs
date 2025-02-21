using MediatR;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Applications.Restaurants.Commands.UpdateRestaurants
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, int>
    {
        private readonly IRestaurantsRepository _restaurantRepository;

        public UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<int> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetOneAsync(request.Id);

            if (restaurant == null)
            {
                throw new NotFoundException($"Restaurant with id {request.Id} not found");
            }

            restaurant.Name = request.Name;
            restaurant.Description = request.Description;
            restaurant.HasDelivery = request.HasDelivery;
            await _restaurantRepository.UpdateAsync(restaurant);

            return restaurant.Id;
        }
    }
}