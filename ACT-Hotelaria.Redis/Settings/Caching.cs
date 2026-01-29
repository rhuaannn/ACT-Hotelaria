using ACT_Hotelaria.Redis.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace ACT_Hotelaria.Redis.Settings;

public class Caching : ICaching
{
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _options;

    public Caching(IDistributedCache distributedCache, IOptions<Settings> cacheSettings)
    {
        _distributedCache = distributedCache;

        var settings = cacheSettings.Value;

        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(settings.AbsoluteExpirationInHours),
            SlidingExpiration = TimeSpan.FromMinutes(settings.SlidingExpirationInMinutes)
        };
    }
    
    public async Task<string?> GetAsync(string key)
    {
        return await _distributedCache.GetStringAsync(key);
    }

    public async Task SetAsync(string key, string value)
    {
        await _distributedCache.SetStringAsync(key, value, _options);
    }

    public async Task RemoveAsync(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }
}