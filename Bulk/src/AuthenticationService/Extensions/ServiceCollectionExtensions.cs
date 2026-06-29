using System.Diagnostics;
using System.Text;
using AuthenticationService.Common;
using AuthenticationService.Features.Register;
using AuthenticationService.Persistence;
using AuthenticationService.Persistence.Repositories.RefreshToken;
using AuthenticationService.Persistence.Repositories.UnitOfWork;
using AuthenticationService.Persistence.Repositories.User;
using AuthenticationService.Services.Jwt;
using AuthenticationService.Services.Password;
using AuthenticationService.Services.RefreshToken;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

namespace AuthenticationService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddEndpointsApiExplorer();

        //#region Controllers
        //services.AddControllers();
        //#endregion

        #region OpenAPI + Scalar (.NET 10)

        services.AddOpenApi();

        #endregion

        #region Database

        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));
        });

        #endregion

        #region Jwt Settings

        services.Configure<JwtSettings>(
            configuration.GetSection("Jwt"));

        #endregion

        #region Authentication

        var jwt = configuration.GetSection("Jwt");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt["Key"]!))
                };
            });

        services.AddAuthorization();

        #endregion

        #region MediatR

        services.AddMediatR(typeof(RegisterCommandHandler).Assembly);

        #endregion

        #region FluentValidation

        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

        #endregion

        #region Repositories

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        #endregion

        #region Services

        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        #endregion

        #region Unit of Work

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion

        return services;
    }
}