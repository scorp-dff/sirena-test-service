using Application.Services.ProviderOneService;
using Application.Services.ProviderTwoService;
using Infrastructure.Common.Constants;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddHttpClient(HttpClientNames.ProviderOne, httpClient =>
            {
                var url = configuration.GetSection("ProviderOne:Url").Value;
                ArgumentNullException.ThrowIfNull(url);
                httpClient.BaseAddress = new Uri(url);
            });

            services.AddHttpClient(HttpClientNames.ProviderTwo, httpClient =>
            {
                var url = configuration.GetSection("ProviderTwo:Url").Value;
                ArgumentNullException.ThrowIfNull(url);
                httpClient.BaseAddress = new Uri(url);
            });

            services.AddScoped<IProviderOneService, ProviderOneService>();
            services.AddScoped<IProviderTwoService, ProviderTwoService>();

            return services;
        }
    }
}
