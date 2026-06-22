using MediatR;

namespace UserProfileService.Features.UserProfiles.CreateUserProfile;

public record CreateUserProfileCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    IFormFile? Image
) : IRequest<Guid>;

#region Validation
#endregion