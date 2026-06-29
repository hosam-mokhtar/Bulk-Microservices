using AuthenticationService.Entities;
using Microsoft.EntityFrameworkCore;
using RefreshTokenEntity = AuthenticationService.Entities.RefreshToken;

namespace AuthenticationService.Persistence.Repositories.RefreshToken
{
    public interface IRefreshTokenRepository
    {
        public void Add(RefreshTokenEntity refreshToken);
        Task<RefreshTokenEntity?> GetByTokenAsync(string token, CancellationToken cancellationToken);

    }
}
