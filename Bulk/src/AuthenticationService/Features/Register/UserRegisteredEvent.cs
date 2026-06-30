namespace AuthenticationService.Features.Register;

public record UserRegisteredEvent(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);
