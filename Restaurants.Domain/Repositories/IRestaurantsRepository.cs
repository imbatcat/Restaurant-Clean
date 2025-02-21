using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync(Expression<Func<Restaurant, bool>>? predicate = null);

        Task<Restaurant?> GetOneAsync(int id);

        Task<int> Create(Restaurant restaurant);

        Task DeleteAsync(Restaurant restaurant);

        Task<Restaurant> PatchAsync(Restaurant restaurant);

        Task<Restaurant> UpdateAsync(Restaurant restaurant);

        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection);
    }
}