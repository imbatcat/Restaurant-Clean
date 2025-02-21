using Microsoft.EntityFrameworkCore;
using Restaurants.Applications.Specifications;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();
            var baseQuery = dbContext.Restaurants
                .Where(r =>
                searchPhraseLower == null
                || r.Name.ToLower().Contains(searchPhraseLower)
                || r.Description.ToLower().Contains(searchPhraseLower));

            if (sortBy != null)
            {
                var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r => r.Name },
                    { nameof(Restaurant.Description), r => r.Description },
                    { nameof(Restaurant.Category), r => r.Category }
                };

                var selectedColumn = columnSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var totalCount = await baseQuery.CountAsync();

            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
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

        public async Task<Restaurant> UpdateAsync(Restaurant restaurant)
        {
            var updatedEntity = dbContext.Update(restaurant).Entity;
            dbContext.Entry(restaurant).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return restaurant;
        }
    }
}