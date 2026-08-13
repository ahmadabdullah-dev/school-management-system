using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAccess;
public static class DependencyInjection{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine,LogLevel.Information).EnableSensitiveDataLogging();
        });

        services.AddIdentityCore<IdentityUser>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();

        services.AddDataProtection();

        services.AddScoped<ISettingsRepository, SettingsRepository>();

        return services;
    }
}