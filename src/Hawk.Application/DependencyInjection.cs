namespace Hawk.Application;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }
}
