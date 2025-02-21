namespace MindSpace.Infrastructure.Seeders.FakeData;

using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;

internal static class IdentityData
{
    public static IEnumerable<User> GetUsers()
    {
        List<User> users = new List<User>
        {
            new User
            {
                UserName = "student1",
                NormalizedUserName = "STUDENT1",
                Email = "student1@example.com",
                NormalizedEmail = "STUDENT1@EXAMPLE.COM",
                Nationality = "German",
                DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow),
            },
        };

        return users;
    }

    public static IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new("ADMIN")
            {
                NormalizedName = "ADMIN"
            },
        };

        return roles;
    }
}