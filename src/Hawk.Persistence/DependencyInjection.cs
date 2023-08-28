namespace Hawk.Persistence;

using Hawk.Domain.Interfaces;
using Hawk.Persistence.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        var cfg = new Configuration();
        cfg.Configure(assembly, "Hawk.Persistence.hibernate.cfg.xml");
        cfg.DataBaseIntegration(x =>
        {
            x.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            x.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
        });

        foreach (var resourceName in assembly.GetManifestResourceNames())
        {
            if (resourceName.EndsWith(".hbm.xml"))
            {
                using var stream = assembly.GetManifestResourceStream(resourceName);
                cfg.AddInputStream(stream);
            }
        }

        new SchemaExport(cfg).Create(true, true);

        var sessionFactory = cfg.BuildSessionFactory();

        services.AddSingleton(sessionFactory);
        services.AddScoped(factory => sessionFactory.OpenSession());

        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}
