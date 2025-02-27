using AutoMapper;
using Restaurants.Applications.Dishes.Commands.CreateDishes;
using Restaurants.Domain.Entities;

namespace Restaurants.Applications.Dishes.Dtos
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDto>();
        }
    }
}
