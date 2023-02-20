using Infrastructure.Persistence;

namespace SirenaDemoService
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
            services.AddHealthChecks()    
                .AddDbContextCheck<ApplicationDbContext>();

            return services;
        }
    }
}
