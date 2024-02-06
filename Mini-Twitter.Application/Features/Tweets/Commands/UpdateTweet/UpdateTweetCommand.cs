namespace Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweet
{
    public class UpdateTweetCommand : IRequest<TweetDto?>
    {
        public int Id { get; set; }
        public UpdateTweetDto UpdateTweetDto { get; set; }
    }
}
