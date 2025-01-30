using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Authorization.Requirements;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Services;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantDb");
            services.AddDbContextPool<RestaurantDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging();
            });
            #region Identities
            //identity api endpoints already handle 
            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RestaurantDbContext>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipleFactory>();
            #endregion Identities

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRestaurantsRepository, RestaurantRepository>();
            services.AddScoped<IDishesRepository, DishesRepositories>();
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

            #region Policies

            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Polish"))
                .AddPolicy(PolicyNames.AtLeast20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.OwnsAtLeastTwoRestaurants, builder => builder.AddRequirements(new MinimumRestaurantOwnership(2)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, MinimumRestaurantOwnershipHandler>();

            #endregion Policies
        }
    }
}