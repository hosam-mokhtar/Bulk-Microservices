namespace AuthenticationService.Features.Login
{
    public sealed record LoginResponse(
        string AccessToken,
        string RefreshToken,
        bool ProfileCompleted,
        bool IsPremium);
}
