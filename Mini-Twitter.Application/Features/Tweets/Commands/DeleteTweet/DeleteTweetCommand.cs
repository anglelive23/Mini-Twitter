namespace Mini_Twitter.Application.Features.Tweets.Commands.DeleteTweet
{
    public class DeleteTweetCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
