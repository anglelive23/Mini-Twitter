namespace Mini_Twitter.Application.Features.Tweets.Commands.DeleteTweet
{
    public class DeleteTweetCommandValidator : AbstractValidator<DeleteTweetCommand>
    {
        public DeleteTweetCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
