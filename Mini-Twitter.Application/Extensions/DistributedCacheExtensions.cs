using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Mini_Twitter.Application.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<IQueryable<T>> GetOrSetAsync<T>(
            this IDistributedCache cache,
            string key,
            Func<CancellationToken, Task<IQueryable<T>>> fallbackFunction,
            DistributedCacheEntryOptions options,
            CancellationToken ct) where T : class
        {
            var cachedData = await cache.GetStringAsync(key, ct);
            if (!string.IsNullOrEmpty(cachedData))
                return JsonSerializer.Deserialize<List<T>>(cachedData).AsQueryable();

            var callbackData = await fallbackFunction(ct);
            await cache.SetStringAsync(key, JsonSerializer.Serialize(callbackData), options, ct);
            return callbackData;
        }
    }
}
