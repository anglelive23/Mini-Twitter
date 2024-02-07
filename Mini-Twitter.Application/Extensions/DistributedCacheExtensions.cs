using System.Text.Json;

namespace Mini_Twitter.Application.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<IQueryable<T>> GetOrSetAsync<T>(
            this IDistributedCache cache,
            string key,
            Func<Task<IQueryable<T>>> fallbackFunction,
            DistributedCacheEntryOptions options,
            CancellationToken ct) where T : class
        {
            var cachedData = await cache.GetStringAsync(key, ct);
            if (!string.IsNullOrEmpty(cachedData))
                return DeserializeData<T>(cachedData).AsQueryable();

            var callbackData = await fallbackFunction();
            var serlizedData = JsonSerializer.Serialize(callbackData);
            await cache.SetStringAsync(key, serlizedData, options, ct);
            return DeserializeData<T>(serlizedData).AsQueryable();
        }

        private static List<T> DeserializeData<T>(string data) where T : class
        {
            return JsonSerializer.Deserialize<List<T>>(data)!;
        }
    }
}
