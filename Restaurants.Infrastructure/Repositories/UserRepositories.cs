using Microsoft.EntityFrameworkCore;
using Restaurants.Applications.Specifications;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Specifications;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class UserRepositories(RestaurantDbContext dbContext) : IUserRepository
    {
        public IQueryable<User> EvaluateSpecification(ISpecification<User> specification)
        {
            return SpecificationEvaluator<User>.GetQuery(dbContext.Users.AsQueryable(), specification);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllWithSpecAsync(ISpecification<User>? specification)
        {
            var query = EvaluateSpecification(specification);
            return await query.ToListAsync();
        }
    }
}