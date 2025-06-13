﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Mini_Twitter.Application.Models.Dtos;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Mini_Twitter.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        #region Fields and Properties
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _contextAccessor;
        private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        public CacheService(IDistributedCache cache, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        #endregion

        #region Interface Implementation
        public async Task<T> GetAsync<T>(
            string? key,
            Func<Task<T>> fallbackFunction,
            DistributedCacheEntryOptions options,
            CancellationToken cancellationToken = default) where T : class
        {
            // If cache connection is not established, fallback to the function and return the data
            // could use ConnectionMultiplexer to check if the connection is established, but for simplicity we will just wrap inside try-catch
            // since GetStringAsync will return timed out
            try
            {
                // If key is null, generate it based on the type of T
                var cacheKey = key ?? GenerateCacheKey(typeof(T));
                var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);
                if (!string.IsNullOrEmpty(cachedData))
                    return JsonSerializer.Deserialize<T>(cachedData)!;

                var callbackData = await fallbackFunction();
                await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(callbackData), options, cancellationToken);
                // Add the cachekey to the dict to handle delete events later on
                CacheKeys.TryAdd(cacheKey, true);
                return callbackData;
            }
            catch (RedisConnectionException)
            {
                var data = await fallbackFunction();
                return data;
            }
        }

        /// <summary>
        /// Deletes all set of keys from cache for a given prefix, 
        /// works that way because we can have multiple cache keys for the same exact entity due to using different odata operations. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public async Task RemoveByPrefixAsync(string prefix, CancellationToken cancellationToken = default)
        {
            IEnumerable<Task>? tasks = CacheKeys
                .Keys
                .Where(k => k.StartsWith(prefix))
                .Select(k => RemoveAsync(k, cancellationToken));

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Delete cached values for a given key.
        /// You can use this directly if you have the exact key, if else better use RemoveByPrefixAsync.
        /// </summary>
        /// <param name="key">cache key of the items to be removed.</param>
        /// <returns></returns>
        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
            CacheKeys.TryRemove(key, out bool _);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Generate cache key based on the type of the passed argument
        /// </summary>
        /// <param name="type">Type to generate cache key for.</param>
        /// <returns>the generated key for the given type.</returns>
        private string GenerateCacheKey(Type type)
        {
            var query = _contextAccessor.HttpContext!.Request.QueryString.Value;
            string cacheKey;
            switch (type)
            {
                case Type tweetType when tweetType == typeof(List<TweetDto>):
                    cacheKey = string.IsNullOrEmpty(query)
                        ? $"{Constants.TweetsKey}"
                        : $"{Constants.TweetsKey}-{query}";
                    break;

                case Type tweetType when tweetType == typeof(TweetDto):
                    {
                        var routeParameters = _contextAccessor.HttpContext.Request.RouteValues;
                        var key = routeParameters["key"];
                        cacheKey = string.IsNullOrEmpty(query)
                            ? $"{Constants.TweetKey}{key}"
                            : $"{Constants.TweetKey}{query}";
                    }
                    break;
                case Type userType when userType == typeof(ApplicationUserDto):
                    {
                        var key = _contextAccessor.HttpContext.Request.RouteValues["key"];
                        cacheKey = string.IsNullOrEmpty(query)
                            ? $"{Constants.UserKey}-{key}"
                            : $"{Constants.UserKey}-{key}-{query}";
                    }
                    break;
                default:
                    cacheKey = string.Empty;
                    break;
            }
            return cacheKey;
        }
        #endregion
    }
}
