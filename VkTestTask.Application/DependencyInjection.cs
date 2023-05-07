using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VkTestTask.Application.Common.Settings;
using VkTestTask.Application.Services.Implementations;
using VkTestTask.Application.Services.Interfaces;


namespace VkTestTask.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        services.Configure<JwtSettings>(settings =>
        {
            configuration.GetSection(JwtSettings.SectionName).Bind(settings);
        });
        return services;
    }
}
