using AuthenticationService.Extensions;
using AuthenticationService.Features.Register;
using MassTransit;
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

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.Message<UserRegisteredEvent>(x =>
                        x.SetEntityName("user-registered-event"));
                });
            });

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
