﻿using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetOneAsync(int id);
        Task<int> Create(Restaurant restaurant);
        Task DeleteAsync(Restaurant restaurant);
        Task<Restaurant> PatchAsync(Restaurant restaurant);
    }
}
