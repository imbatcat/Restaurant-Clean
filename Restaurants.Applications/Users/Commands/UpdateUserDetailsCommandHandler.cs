using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Applications.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Applications.Users.Commands
{
    internal class UpdateUserDetailsCommandHandler(
        ILogger<UpdateUserDetailsCommandHandler> logger,
        IUserContext userContext,
        IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (dbUser == null)
            {
                throw new NotFoundException(nameof(User), user!.Id.ToString());
            }

            dbUser.Nationality = request.Nationality;
            dbUser.DateOfBirth = request.DateOfBirth;

            await userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
