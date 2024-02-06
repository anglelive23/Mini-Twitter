namespace Mini_Twitter.Application.Features.Tweets.Commands.CreateTweet
{
    public class CreateTweetCommand : IRequest<TweetDto>
    {
        public CreateTweetDto TweetDto { get; set; }
    }
}
