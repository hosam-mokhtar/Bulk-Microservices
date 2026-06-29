using AuthenticationService.Common;
using AuthenticationService.Features.Register;
using MediatR;

namespace AuthenticationService.Features.Register
{
    public sealed record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber)
        : IRequest<RequestResult<RegisterResponse>>;
}
