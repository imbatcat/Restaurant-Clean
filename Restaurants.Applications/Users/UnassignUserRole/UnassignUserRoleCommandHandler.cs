
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Applications.Users.UnassignUserRole
{
    public class UnassignUserRoleCommandHandler(
        ILogger<UnassignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
    {
        public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Unassigning role for user {UserEmail}", request.Email);
            var user = await userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(User), request.Email);
            var userRoles = await userManager.GetRolesAsync(user);

            if (!userRoles.Contains(request.UserRole)) throw new NotFoundException($"Role does not exist in user {request.Email}.");

            await userManager.RemoveFromRoleAsync(user, request.UserRole);
        }
    }
}
