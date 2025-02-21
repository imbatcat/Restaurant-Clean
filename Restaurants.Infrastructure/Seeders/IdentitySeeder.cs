namespace MindSpace.Infrastructure.Seeders;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MindSpace.Infrastructure.Seeders.FakeData;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

internal class IdentitySeeder(
    ILogger<IdentitySeeder> logger,
    RestaurantDbContext dbContext,
    UserManager<User> userManager) : IIdentitySeeder
{
    private static readonly string Password = "Password1!";

    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync())
            try
            {
                IEnumerable<User> users = null;
                if (!dbContext.Roles.Any())
                {
                    var roles = IdentityData.GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Users.Any())
                {
                    users = IdentityData.GetUsers();
                    foreach (var user in users) await userManager.CreateAsync(user, Password);
                }
                if (users is not null && !dbContext.UserRoles.Any()) await GetUserRoles(users);
            }
            catch (Exception ex)
            {
                logger.LogError("{ex}", ex.Message);
            }
    }

    private async Task GetUserRoles(IEnumerable<User> users)
    {
        foreach (var user in users)
        {
            await userManager.AddToRoleAsync(user, "ADMIN");
        }
    }
}