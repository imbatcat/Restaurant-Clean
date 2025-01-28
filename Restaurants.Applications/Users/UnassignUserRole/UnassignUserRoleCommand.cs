using MediatR;

namespace Restaurants.Applications.Users.UnassignUserRole
{
    public class UnassignUserRoleCommand : IRequest
    {
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}
