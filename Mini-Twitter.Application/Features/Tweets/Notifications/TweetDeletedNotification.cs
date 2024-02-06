namespace Mini_Twitter.Application.Features.Tweets.Notifications
{
    public class TweetDeletedNotification : INotification
    {
        public required string Key { get; set; }
    }
}
