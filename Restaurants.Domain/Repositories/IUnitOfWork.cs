
namespace Restaurants.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IDishesRepository dishesRepository {get;}
        IRestaurantsRepository restaurantsRepository {get;}
        IUserRepository userRepository {get;}
        Task<int> SaveChangesAsync();
    }
}
