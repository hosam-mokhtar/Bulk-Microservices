using AuthenticationService.Extensions;
using AuthenticationService.Features.Login;
using AuthenticationService.Features.Refresh_Token;
using AuthenticationService.Features.Register;
using Scalar.AspNetCore;

namespace AuthenticationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationServices(builder.Configuration);        

            var app = builder.Build();

            #region Minmal APIs Endpoints

            app.MapAuthenticationEndpoints();

            #endregion

            app.MapOpenApi();
            app.MapScalarApiReference();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapControllers();

            app.Run();
        }
    }
}
