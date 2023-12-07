namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetsList
{
    public class GetUserTweetsListQueryValidator : AbstractValidator<GetUserTweetsListQuery>
    {
        public GetUserTweetsListQueryValidator()
        {
            RuleFor(t => t.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
