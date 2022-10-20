using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telligent.Core.Infrastructure.Database;

namespace Telligent.Admin.Database;

public static class DbContextExtension
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, string connection)
    {
        services.AddDbContext<MemberDbContext>(options =>
        {
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
        });

        return services;
    }

    public static void RegisterDbContexts(this ContainerBuilder builder)
    {
        builder.RegisterType<MemberDbContext>().As<BaseDbContext>().InstancePerLifetimeScope();
    }
}
