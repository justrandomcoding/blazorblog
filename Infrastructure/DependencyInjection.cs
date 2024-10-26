using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        
        services.AddDbContextFactory<BlogDbContext>(
            (s, builder) =>
            {
                var configuration = s.GetRequiredService<IConfiguration>();
                builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
#if DEBUG
                    .EnableDetailedErrors()
#endif
                    ;
            });

        return services;
    }
}