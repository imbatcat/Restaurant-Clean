using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        public IDishesRepository dishesRepository { get; private set; }

        public IRestaurantsRepository restaurantsRepository { get; private set; }

        public IUserRepository userRepository { get; private set; }

        public UnitOfWork(RestaurantDbContext restaurantDbContext)
        {
            _restaurantDbContext = restaurantDbContext;
            dishesRepository = new DishesRepositories(restaurantDbContext);
            restaurantsRepository = new RestaurantRepository(restaurantDbContext);
            userRepository = new UserRepositories(restaurantDbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _restaurantDbContext.SaveChangesAsync();
        }
    }
}