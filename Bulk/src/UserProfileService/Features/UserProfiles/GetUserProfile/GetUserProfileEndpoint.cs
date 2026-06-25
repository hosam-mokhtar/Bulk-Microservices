using Carter;
using MediatR;
using UserProfileService.Contracts.UserProfiles;

namespace UserProfileService.Features.UserProfiles.GetUserProfile;

public class GetUserProfileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/me/{userId:guid}", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetUserProfileQuery(), cancellationToken);

            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
        })
         .WithName("GetUserProfile")
         .WithTags("UserProfile")
         .RequireAuthorization()
         .Produces<UserProfileResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
