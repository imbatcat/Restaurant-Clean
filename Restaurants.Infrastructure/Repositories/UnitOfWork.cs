using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        public IDishesRepository dishesRepository { get; private set; }

        public IRestaurantsRepository restaurantsRepository { get; private set; }

        public UnitOfWork(RestaurantDbContext restaurantDbContext, IDishesRepository dishesRepository, IRestaurantsRepository restaurantsRepository)
        {
            _restaurantDbContext = restaurantDbContext;
            this.dishesRepository = new DishesRepositories(restaurantDbContext);
            this.restaurantsRepository = new RestaurantRepository(restaurantDbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _restaurantDbContext.SaveChangesAsync();
        }
    }
}