using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Specifications;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
    {
        private IQueryable<Restaurant> ApplySpecification(Specification<Restaurant> specification)
        {
            return SpecificationEvaluator<Restaurant>.GetQuery(
                dbContext.Restaurants, specification);
        }

        public async Task<int> Create(Restaurant restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            dbContext.Restaurants.Remove(restaurant);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync(Expression<Func<Restaurant, bool>>? predicate = null)
        {
            var query = dbContext.Restaurants.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            var restaurants = await query.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetOneAsync(int id)
        {
            var specification = new GetRestaurantWithDishesSpecification(id);
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<Restaurant> PatchAsync(Restaurant restaurant)
        {
            var entry = dbContext.Attach(restaurant);
            entry.Property(x => x.Name).IsModified = true;
            entry.Property(x => x.Description).IsModified = true;
            entry.Property(x => x.HasDelivery).IsModified = true;
            
            await dbContext.SaveChangesAsync();
            return restaurant;
        }
    }
}
