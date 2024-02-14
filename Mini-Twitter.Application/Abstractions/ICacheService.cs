namespace Mini_Twitter.Application.Abstractions
{
    public interface ICacheService
    {
        /// <summary>
        /// Always project the IQueryable with ToList in order for this to work out.
        /// If IQueryable is still used as T, Deserialize and Serialize will throw an exception.
        /// </summary>
        /// <typeparam name="T">Return type of the GetAsync.</typeparam>
        /// <param name="key">Cache Key.</param>
        /// <param name="fallbackFunction">Function to call back if cache don't contain cached item with the same key.</param>
        /// <param name="options">Distributed CacheEntry Options, default value is 20s.</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(
            string? key,
            Func<Task<T>> fallbackFunction,
            DistributedCacheEntryOptions options,
            CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Deletes all set of keys from cache for a given prefix, 
        /// works that way because we can have multiple cache keys for the same exact entity due to using different odata operations. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete cached values for a given key.
        /// You can use this directly if you have the exact key, if else better use RemoveByPrefixAsync.
        /// </summary>
        /// <param name="key">cache key of the items to be removed.</param>
        /// <returns></returns>
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
