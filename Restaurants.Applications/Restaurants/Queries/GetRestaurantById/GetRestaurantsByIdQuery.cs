using MediatR;
using Restaurants.Applications.Restaurants.Dtos;

namespace Restaurants.Applications.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantsByIdQuery(int id): IRequest<RestaurantDto>
    {
        public int Id { get; } = id;
    }
}
