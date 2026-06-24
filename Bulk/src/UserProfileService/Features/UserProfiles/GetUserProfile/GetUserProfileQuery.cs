using MediatR;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Abstractions;
using UserProfileService.Contracts.UserProfiles;
using UserProfileService.Entities;
using UserProfileService.Errors;
using UserProfileService.Interfaces;

namespace UserProfileService.Features.UserProfiles.GetUserProfile;

public record GetUserProfileQuery(
    Guid UserId 
) : IRequest<Result<UserProfileResponse>>;

internal class GetUserProfileQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetUserProfileQuery, Result<UserProfileResponse>>
{
    public async Task<Result<UserProfileResponse>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .Repository<UserProfile>()
            .GetQueryable()
            .Where(x => x.UserId == request.UserId)
            .Select(x => new UserProfileResponse(
                x.UserId,
                x.FirstName,
                x.LastName,
                x.Email,
                x.Phone
            )).FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            return Result.Failure<UserProfileResponse>(UserErrors.NotFound(request.UserId));

        return Result.Success(user);
    }
}