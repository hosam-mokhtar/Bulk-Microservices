using System.Reflection;
using UserProfileService.Abstractions;

namespace UserProfileService.Extensions;

public static class EndpointExtension
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var endpointTypes = assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpoint)) && !t.IsAbstract);

        foreach (var type in endpointTypes)
            services.AddSingleton(typeof(IEndpoint), type);

        return services;
    }
}
