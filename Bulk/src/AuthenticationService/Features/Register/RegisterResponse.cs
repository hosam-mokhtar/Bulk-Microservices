namespace AuthenticationService.Features.Register
{
    public sealed record RegisterResponse(
                  Guid UserId,
                  bool RequiresProfileCompletion);
}
