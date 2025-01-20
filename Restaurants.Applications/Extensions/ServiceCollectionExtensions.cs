using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Applications.Restaurants;
using Restaurants.Applications.Users;

namespace Restaurants.Applications.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplications(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddMediatR(mtr => mtr.RegisterServicesFromAssembly(applicationAssembly));
            //auto mapper and validator will go through all the classes in the appointted assembly to search for appropriate class needed for mapping/validating
            services.AddAutoMapper(applicationAssembly);
            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();
            //allow HttpContextAccessor injection
            services.AddHttpContextAccessor();
        }
    }
}
