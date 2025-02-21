using MediatR;
using Restaurants.Applications.Common;
using Restaurants.Applications.Restaurants.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Applications.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
    {
        public string? searchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
