using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;


namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepositories(RestaurantDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAllAsync(Expression<Func<Dish, bool>>? predicate)
        {
            var query = dbContext.Dishes.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            dbContext.Dishes.RemoveRange(query);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dish>> GetAllAsync(Expression<Func<Dish, bool>>? predicate)
        {
            var query = dbContext.Dishes.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }
    }
}
