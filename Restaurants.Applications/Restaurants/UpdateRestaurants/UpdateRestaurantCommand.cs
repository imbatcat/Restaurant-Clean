using MediatR;

namespace Restaurants.Applications.Restaurants.Commands.UpdateRestaurants
{
    public record UpdateRestaurantCommand : IRequest<int>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool HasDelivery { get; init; }
    }
}