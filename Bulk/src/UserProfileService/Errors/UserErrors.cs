using UserProfileService.Abstractions;

namespace UserProfileService.Errors;

public static class UserErrors
{
    public static Error NotFound(Guid userId) =>
        new("User.NotFound", $"user with id '{userId}' was not found.", StatusCodes.Status404NotFound);
}
