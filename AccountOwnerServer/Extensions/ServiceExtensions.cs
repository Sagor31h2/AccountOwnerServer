using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AccountOwnerServer.Extensions
{
    public static class ServiceExtensions
    {
        //Cors for recive json from domain
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            //iis configuration
            services.Configure<IISOptions>(options =>
            {
            });

        }
    }
}