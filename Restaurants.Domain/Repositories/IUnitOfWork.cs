
namespace Restaurants.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IDishesRepository dishesRepository {get;}
        IRestaurantsRepository restaurantsRepository {get;}
        Task<int> SaveChangesAsync();
    }
}
