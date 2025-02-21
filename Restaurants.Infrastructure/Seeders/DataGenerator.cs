using Bogus;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;

public static class DataGenerator
{
    // Use static seed to ensure consistent data generation
    private static readonly int Seed = 42;

    public static List<Restaurant> Restaurants = new();
    public static List<Dish> Dishes = new();
    public static List<Address> Addresses = new();
    private static int _restaurantId = 1;
    private static int _dishId = 1;
    private static readonly string[] FixedOwnerIds = ["d8f1b82e-f13b-446d-9b64-5563ddc79dff", "c125c9c1-9907-47b0-8a34-be8d39cd4774"];
    private static readonly string[] Categories = { "Italian", "Mexican", "Chinese", "American", "Japanese" };

    public static void Init(int count)
    {
        // Clear existing collections to prevent
        Restaurants.Clear();
        Addresses.Clear();
        Dishes.Clear();

        // Reset IDs
        _restaurantId = 1;
        _dishId = 1;
        // Set a fixed seed for Faker to generate consistent data
        Randomizer.Seed = new Random(Seed);

        var dishFaker = new Faker<Dish>()
            .RuleFor(d => d.Id, f => _dishId++)
            .RuleFor(d => d.Name, f => f.Commerce.ProductName())
            .RuleFor(d => d.Description, f => f.Lorem.Sentence())
            .RuleFor(d => d.Price, f => f.Random.Decimal(5, 100))
            .RuleFor(d => d.KiloCalories, f => f.Random.Int(100, 1000).OrNull(f, 0.2f));

        var addressFaker = new Faker<Address>()
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Street, f => f.Address.StreetAddress())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());

        var restaurantFaker = new Faker<Restaurant>()
            .RuleFor(r => r.Id, f => _restaurantId++)
            .RuleFor(r => r.Name, f => f.Company.CompanyName())
            .RuleFor(r => r.Description, f => f.Lorem.Paragraph())
            .RuleFor(r => r.Category, f => f.PickRandom(Categories))
            .RuleFor(r => r.HasDelivery, f => f.Random.Bool())
            .RuleFor(r => r.ContactEmail, f => f.Internet.Email())
            .RuleFor(r => r.Contactnumber, f => f.Phone.PhoneNumber())
            .RuleFor(r => r.OwnerId, f => f.PickRandom(FixedOwnerIds));

        // Generate restaurants first
        var restaurants = restaurantFaker.Generate(count);
        Restaurants.AddRange(restaurants);
        Console.WriteLine(Restaurants[0].Id + " " + Restaurants[0].Name);

        // Generate addresses for each restaurant
        var addresses = restaurants.Select(r =>
        {
            addressFaker.RuleFor(a => a.RestaurantId, r.Id);
            return addressFaker.Generate();
        }).ToList();
        Addresses.AddRange(addresses);

        // Generate dishes for each restaurant
        foreach (var restaurant in restaurants)
        {
            var dishes = dishFaker
                .RuleFor(d => d.RestaurantId, restaurant.Id)
                .GenerateBetween(3, 8);

            Dishes.AddRange(dishes);
        }
    }
}