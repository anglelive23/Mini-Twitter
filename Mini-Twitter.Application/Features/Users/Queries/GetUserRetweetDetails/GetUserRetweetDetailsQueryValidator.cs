namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetDetails
{
    public class GetUserRetweetDetailsQueryValidator : AbstractValidator<GetUserRetweetDetailsQuery>
    {
        public GetUserRetweetDetailsQueryValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(u => u.RetweetId).NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
