using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Specifications.Composite;
using Restaurants.Applications.Users.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Applications.Users.Queries
{
    public class GetAllUserWithAtLeastThreeRestaurantsAndAtMostFiveDishesQueryHandler
        (ILogger<GetAllUserWithAtLeastThreeRestaurantsAndAtMostFiveDishesQueryHandler> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper): IRequestHandler<GetAllUserWithAtLeastThreeRestaurantsAndAtMostFiveDishesQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllUserWithAtLeastThreeRestaurantsAndAtMostFiveDishesQuery request, CancellationToken cancellationToken)
        {
            var specification = new OwnerHasAtLeastThreeRestaurantsWithAtMostFiveDishesSpecification();
            var users = await unitOfWork.userRepository.GetAllWithSpecAsync(specification);
            
            var userDtos = mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }
    }
}