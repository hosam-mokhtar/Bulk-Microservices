using Carter;
using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Features.UserProfiles.Messaging.Consumers;
using UserProfileService.Interfaces;
using UserProfileService.Persistence;

namespace UserProfileService;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddRabbitMqConfiguration();
        services.AddCarter();

        var assembly = typeof(DependencyInjection).Assembly;

        //Add Mapster
        var mappingConfiguration = TypeAdapterConfig.GlobalSettings;
        mappingConfiguration.Scan(assembly);

        services.AddSingleton<IMapper>(new Mapper(mappingConfiguration));

        //Add MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }

    private static IServiceCollection AddRabbitMqConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumer<UserRegisteredConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("user-registered-queue", e =>
                {
                    e.ConfigureConsumer<UserRegisteredConsumer>(context);
                });
            });
        });

        return services;
    }
}
