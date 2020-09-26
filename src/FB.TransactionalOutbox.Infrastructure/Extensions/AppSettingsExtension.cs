using FB.TransactionalOutbox.Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FB.TransactionalOutbox.Infrastructure.Extensions
{
    public static class AppSettingsExtension
    {
        public static void AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            var appSettingsSection = configuration.GetSection("AppSettings");
            appSettingsSection.Bind(appSettings);
            
            services.Configure<AppSettings>(appSettingsSection);
        }
    }
}