namespace Mini_Twitter.Application.Extensions
{
    public class CacheOptions
    {
        public static DistributedCacheEntryOptions DefaultExpiration => new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) };
    }
}
