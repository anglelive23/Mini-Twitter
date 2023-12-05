namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetDetails
{
    public class GetTweetDetailsQueryValidator : AbstractValidator<GetTweetDetailsQuery>
    {
        public GetTweetDetailsQueryValidator()
        {
            RuleFor(l => l.Id)
                .NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
