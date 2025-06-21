using App.Application.Contracts.Caching;
using App.Caching;

namespace CleanApp.API.Extension;

public static class CacheExtensions
{
    public static IServiceCollection AddCachingExt(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
