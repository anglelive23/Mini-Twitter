namespace Mini_Twitter.Application.Features.Tweets.Commands.DeleteTweetReply
{
    public class DeleteTweetReplyCommand : IRequest<bool>
    {
        public int TweetId { get; set; }
        public int ReplyId { get; set; }
    }
}
