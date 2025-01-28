
using Restaurants.Domain.Entities;
using System.Linq.Expressions;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
        Task<IEnumerable<Dish>> GetAllAsync(Expression<Func<Dish, bool>>? predicate = null);
        Task DeleteAllAsync(Expression<Func<Dish, bool>>? predicate);
    }
}
