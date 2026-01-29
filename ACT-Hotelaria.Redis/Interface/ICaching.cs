using Microsoft.Extensions.Caching.Distributed;

namespace ACT_Hotelaria.Redis.Repository;

public interface ICaching
{
    Task SetAsync(string key, string value);
    Task<string?> GetAsync(string key);
    Task RemoveAsync(string key);
}
