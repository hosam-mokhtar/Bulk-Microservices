using AuthenticationService.Entities;

namespace AuthenticationService.Features.Logout
{
    public sealed record LogoutRequest(string RefreshToken);
}
