namespace Mini_Twitter.Application.Features.Tweets.Commands.CreateTweet
{
    public class CreateTweetCommand : IRequest<Tweet>
    {
        public CreateTweetDto TweetDto { get; set; }
    }
}
