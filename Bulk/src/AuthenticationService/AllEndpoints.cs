using AuthenticationService.Features.Login;
using AuthenticationService.Features.Logout;
using AuthenticationService.Features.Refresh_Token;
using AuthenticationService.Features.Register;

namespace AuthenticationService
{
    public static class AllEndpoints
    {
        public static IEndpointRouteBuilder MapAuthenticationEndpoints(
          this IEndpointRouteBuilder app)
        {
            app.MapRegisterEndpoint();
            app.MapLoginEndpoint();
            app.MapRefreshTokenEndpoint();
            app.MapLogoutEndpoint();

            return app;
        }
    }
}
