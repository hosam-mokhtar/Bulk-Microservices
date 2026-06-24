using MediatR;
using UserProfileService.Entities;
using UserProfileService.Interfaces;

namespace UserProfileService.Features.UserProfiles.CreateUserProfile;

public class CreateUserProfileCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserProfileCommand, bool>
{
    public async Task<bool> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = new UserProfile
        {
            UserId = Guid.CreateVersion7(),
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone
        };

        var preferences = new UserPreference { UserId = request.UserId };
        var notifications = new NotificationSetting { UserId = request.UserId };
        var privacy = new PrivacySetting { UserId = request.UserId };

        await unitOfWork.Repository<UserProfile>().AddAsync(userProfile, cancellationToken);
        await unitOfWork.Repository<UserPreference>().AddAsync(preferences, cancellationToken);
        await unitOfWork.Repository<NotificationSetting>().AddAsync(notifications, cancellationToken);
        await unitOfWork.Repository<PrivacySetting>().AddAsync(privacy, cancellationToken);

        var rowsAffected = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (rowsAffected == 0)
            return false;

        return true;
    }
}
