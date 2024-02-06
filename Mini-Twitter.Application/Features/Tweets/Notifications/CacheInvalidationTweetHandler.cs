namespace Mini_Twitter.Application.Features.Tweets.Notifications
{
    public class CacheInvalidationTweetHandler
        : INotificationHandler<TweetCreatedNotification>
        , INotificationHandler<TweetDeletedNotification>
        , INotificationHandler<TweetUpdatedNotification>

    {
        #region Fields and Properties
        private readonly IDistributedCache _cache;
        #endregion

        #region Constructors
        public CacheInvalidationTweetHandler(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        #endregion

        #region Interface Implementations
        public Task Handle(TweetCreatedNotification notification, CancellationToken cancellationToken)
        {
            return HandleInternal(notification.Key);
        }

        public Task Handle(TweetDeletedNotification notification, CancellationToken cancellationToken)
        {
            return HandleInternal(notification.Key);
        }

        public Task Handle(TweetUpdatedNotification notification, CancellationToken cancellationToken)
        {
            return HandleInternal(notification.Key);
        }
        #endregion

        #region Helper Methods
        private async Task HandleInternal(string key)
        {
            await _cache.RemoveAsync(key);
        }
        #endregion
    }
}
