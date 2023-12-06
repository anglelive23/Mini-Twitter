namespace Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweet
{
    public class UpdateTweetCommandValidator : AbstractValidator<UpdateTweetCommand>
    {
        public UpdateTweetCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.UpdateTweetDto.Context)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MaximumLength(250).WithMessage("{PropertyName} has max length of 250!");
        }
    }
}
