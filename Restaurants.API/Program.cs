using MindSpace.Infrastructure.Seeders;
using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Applications.Extensions;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplications();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

var app = builder.Build();

var scope = app.Services.CreateScope();
var applicationDbContext = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
var identitySeeder = scope.ServiceProvider.GetRequiredService<IIdentitySeeder>();
var sender = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

//await sender.Seed();
await identitySeeder.SeedAsync();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");
//All in all, this line adds the Identity apis to application, groups the identity api's prefix to "api/identity", tag them "Identity" which matches the controller's (IdentityController)
//// Create a route group with the base path "api/identity"
app.MapGroup("api/identities")
    // Add a tag "Identity" for documentation purposes
    .WithTags("identities");
// Map identity-related API endpoints for the User entity

//Not needed since Identity handles that part
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();