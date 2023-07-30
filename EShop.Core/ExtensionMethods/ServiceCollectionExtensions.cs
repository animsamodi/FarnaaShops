using EShop.Core.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Core.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCacheServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ICacheService, CacheService>();
            services
                // Redis Cache Service
                .AddDistributedMemoryCache()
                .AddStackExchangeRedisCache(option =>
                {
                  
                    option.Configuration =   configuration.GetConnectionString("redis");
                });

            return services;
        }
    }
}