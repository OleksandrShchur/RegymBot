using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace RegymBot.AppSettings
{
    public static class ConfigurationExtention
    {
        public static void AddConfigurationProvider(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            services.Configure<JWTSettings>(config.GetSection("JWTSettings"));
        }
    }
}