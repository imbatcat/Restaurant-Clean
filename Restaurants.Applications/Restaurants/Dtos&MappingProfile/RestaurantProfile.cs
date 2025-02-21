using AutoMapper;
using Restaurants.Applications.Restaurants.Commands.CreateRestaurants;
using Restaurants.Applications.Restaurants.Commands.PatchRestaurants;
using Restaurants.Domain.Entities;

namespace Restaurants.Applications.Restaurants.Dtos
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<PatchRestaurantCommand, Restaurant>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(d => d.HasDelivery, opt => opt.MapFrom(src => src.HasDelivery));
            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }));
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
        }
    }
}
