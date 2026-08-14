using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(
        this IServiceCollection services)
    {
        services.AddScoped<ISettingsService,SettingsService>();
        services.AddScoped<IAuthService,AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStudentsService, StudentsService>();
       
        return services;
    }
}
