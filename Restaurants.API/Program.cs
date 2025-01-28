using Restaurants.Infrastructure.Extensions;
using Restaurants.Applications.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Microsoft.OpenApi.Models;
using Restaurants.API.Extensions;
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
var sender = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

await sender.Seed();
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

//All in all, this line adds the Identity apis to application, groups the identity api's prefix to "api/identity", tag them "Identity" which matches the controller's (IdentityController) 
//// Create a route group with the base path "api/identity"
app.MapGroup("api/identity")
    // Add a tag "Identity" for documentation purposes
    .WithTags("Identity")
    // Map identity-related API endpoints for the User entity
    .MapIdentityApi<User>();

//Not needed since Identity handles that part
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
