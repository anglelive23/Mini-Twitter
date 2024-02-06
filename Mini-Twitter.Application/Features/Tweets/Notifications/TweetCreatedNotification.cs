namespace Mini_Twitter.Application.Features.Tweets.Notifications
{
    public class TweetCreatedNotification : INotification
    {
        public required string Key { get; set; }
    }
}
