using AuthenticationService.Common;
using MediatR;

namespace AuthenticationService.Features.Login
{
    public sealed record LoginCommand(string Email, string Password)
        : IRequest<RequestResult<LoginResponse>>;

}
