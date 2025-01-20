using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
    {
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

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetOneAsync(int id)
        {
            var restaurant = await dbContext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id);

            return restaurant;
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
