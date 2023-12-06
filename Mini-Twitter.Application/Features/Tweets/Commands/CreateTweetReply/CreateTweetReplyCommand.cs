namespace Mini_Twitter.Application.Features.Tweets.Commands.CreateTweetReply
{
    public class CreateTweetReplyCommand : IRequest<Reply?>
    {
        public int Id { get; set; }
        public CreateReplyDto ReplyDto { get; set; }
    }
}
