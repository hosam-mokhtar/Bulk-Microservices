using UserProfileService.Abstractions;

namespace UserProfileService.Errors;

public static class UserErrors
{
    public static readonly Error NotFound =
        new("User.NotFound", $"user with id was not found.", StatusCodes.Status404NotFound);
}
