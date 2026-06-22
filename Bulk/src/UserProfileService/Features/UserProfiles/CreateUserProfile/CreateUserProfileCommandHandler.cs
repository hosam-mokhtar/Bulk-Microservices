using MediatR;
using UserProfileService.Entities;
using UserProfileService.Interfaces.Repositories;

namespace UserProfileService.Features.UserProfiles.CreateUserProfile;

public class CreateUserProfileCommandHandler(IUserProfileRepository<UserProfile> repo) : IRequestHandler<CreateUserProfileCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        //grt userId , Email from RabbitMQ messaging from auth service

        var userProfile = new UserProfile
        {
            UserId = Guid.CreateVersion7(),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            ProfilePictureUrl = "imageUrl"
        };

        await repo.AddAsync(userProfile, cancellationToken);
        await repo.SaveChangesAsync(cancellationToken);

        return userProfile.UserId;
    }
}
