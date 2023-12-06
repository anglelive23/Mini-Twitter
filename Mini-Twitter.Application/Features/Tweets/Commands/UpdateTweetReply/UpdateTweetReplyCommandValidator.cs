namespace Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweetReply
{
    public class UpdateTweetReplyCommandValidator : AbstractValidator<UpdateTweetReplyCommand>
    {
        public UpdateTweetReplyCommandValidator()
        {
            RuleFor(r => r.TweetId)
                .NotEmpty().WithMessage("{PropertyName} is required!");

            RuleFor(r => r.ReplyId)
                .NotEmpty().WithMessage("{PropertyName} is required!");

            RuleFor(r => r.UpdateTweetReplyDto.Context)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MaximumLength(250).WithMessage("{PropertyName} has max length of 250");
        }
    }
}
