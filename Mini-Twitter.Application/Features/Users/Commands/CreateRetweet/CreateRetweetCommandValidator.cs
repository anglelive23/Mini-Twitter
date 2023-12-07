namespace Mini_Twitter.Application.Features.Users.Commands.CreateRetweet
{
    public class CreateRetweetCommandValidator : AbstractValidator<CreateRetweetCommand>
    {
        public CreateRetweetCommandValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required!");

            RuleFor(r => r.RetweetDto.TweetId)
                .NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
