using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using SynergyISP.Application;

namespace SynergyISP.Infrastructure.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services)
    {
        ConfigurationOptions configurationOptions = new()
        {
            EndPoints = { "localhost:6379" }, // Unlike aioredis, we don't need to specify "redis://" here
            Ssl = false, // Set this to true if your Redis instance can handle connection using SSL
        };
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.ConfigurationOptions = configurationOptions;
        });

        services.AddSingleton<ICacheService, CacheService>();
        return services;
    }
}
