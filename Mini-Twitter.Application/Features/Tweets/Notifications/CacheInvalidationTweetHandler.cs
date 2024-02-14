namespace Mini_Twitter.Application.Features.Tweets.Notifications
{
    public class CacheInvalidationTweetHandler
        : INotificationHandler<TweetCreatedNotification>
        , INotificationHandler<TweetDeletedNotification>
        , INotificationHandler<TweetUpdatedNotification>

    {
        #region Fields and Properties
        private readonly ICacheService _cacheService;
        #endregion

        #region Constructors
        public CacheInvalidationTweetHandler(ICacheService cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
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
        private async Task HandleInternal(string prefix)
        {
            // Dynamic way to remove all sets of cached keys for the same prefix
            // fits well with odata operations
            await _cacheService.RemoveByPrefixAsync(prefix);
        }
        #endregion
    }
}
