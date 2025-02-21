using AutoMapper;
using Restaurants.Applications.Users.Dtos;
using Restaurants.Applications.Users.Queries;
using Restaurants.Domain.Entities;

namespace Restaurants.Applications.Users.Dtos_MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(nameof(UserDto.UserName), option => option.MapFrom(user => user.UserName))
                .ForMember(nameof(UserDto.Nationality), option => option.MapFrom(dto => dto.Nationality))
                .ForMember(nameof(UserDto.DateOfBirth), option => option.MapFrom(dto => dto.DateOfBirth));
        }
    }
}