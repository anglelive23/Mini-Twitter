namespace Mini_Twitter.Application.Features.Tweets.Commands.DeleteTweetReply
{
    public class DeleteTweetReplyCommandValidator : AbstractValidator<DeleteTweetReplyCommand>
    {
        public DeleteTweetReplyCommandValidator()
        {
            RuleFor(t => t.TweetId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.ReplyId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
