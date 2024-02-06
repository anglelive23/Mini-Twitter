namespace Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweetReply
{
    public class UpdateTweetReplyCommand : IRequest<ReplyDto?>
    {
        public int TweetId { get; set; }
        public int ReplyId { get; set; }
        public UpdateTweetReplyDto UpdateTweetReplyDto { get; set; }
    }
}
