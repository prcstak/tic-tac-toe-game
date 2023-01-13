using interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace application;

public static class ConfigureServices 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IAccountService, AccountService>();
        return services;
    }
}