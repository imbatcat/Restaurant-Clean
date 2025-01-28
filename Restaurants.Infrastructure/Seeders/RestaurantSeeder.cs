using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = [
                new (UserRoles.User) {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Admin) {
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new (UserRoles.Owner) {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
            ];

            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Italian Bistro",
                    Description = "A cozy place for Italian cuisine.",
                    Category = "Italian",
                    HasDelivery = true,
                    ContactEmail = "contact@italianbistro.com",
                    Contactnumber = "123-456-7890",
                    Address = new Address
                    {
                        City = "New York",
                        Street = "5th Avenue",
                        PostalCode = "10001"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Spaghetti Carbonara",
                            Description = "Classic Italian pasta dish.",
                            Price = 12.99m
                        },
                        new Dish
                        {
                            Name = "Margherita Pizza",
                            Description = "Traditional pizza with tomatoes, mozzarella, and basil.",
                            Price = 10.99m
                        }
                    }
                },
                new Restaurant
                {
                    Name = "Sushi Place",
                    Description = "Fresh and delicious sushi.",
                    Category = "Japanese",
                    HasDelivery = false,
                    ContactEmail = "info@sushiplace.com",
                    Contactnumber = "987-654-3210",
                    Address = new Address
                    {
                        City = "San Francisco",
                        Street = "Market Street",
                        PostalCode = "94103"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "California Roll",
                            Description = "Crab, avocado, and cucumber roll.",
                            Price = 8.99m
                        },
                        new Dish
                        {
                            Name = "Salmon Nigiri",
                            Description = "Fresh salmon over rice.",
                            Price = 5.99m
                        }
                    }
                }
            };

            return restaurants;
        }
    }
}