namespace Mini_Twitter.Application.Features.Tweets.Commands.CreateTweet
{
    public class CreateTweetCommandValidator : AbstractValidator<CreateTweetCommand>
    {
        public CreateTweetCommandValidator()
        {
            RuleFor(l => l.TweetDto.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required!");


            RuleFor(l => l.TweetDto.Context)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MaximumLength(250).WithMessage("{PropertyName} has max length of 250!");
        }
    }
}
