using Restaurants.Domain.Entities;
using Restaurants.Domain.Specifications;

namespace Restaurants.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllWithSpecAsync(ISpecification<User>? specification);
        Task<IEnumerable<User>> GetAllAsync();
    }
}