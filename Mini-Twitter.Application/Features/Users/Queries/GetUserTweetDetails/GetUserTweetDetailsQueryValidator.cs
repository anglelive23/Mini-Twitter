namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetDetails
{
    public class GetUserTweetDetailsQueryValidator : AbstractValidator<GetUserTweetDetailsQuery>
    {
        public GetUserTweetDetailsQueryValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(u => u.TweetId).NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
