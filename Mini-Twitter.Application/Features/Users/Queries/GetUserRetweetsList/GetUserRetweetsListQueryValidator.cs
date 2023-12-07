namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetsList
{
    public class GetUserRetweetsListQueryValidator : AbstractValidator<GetUserRetweetsListQuery>
    {
        public GetUserRetweetsListQueryValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
