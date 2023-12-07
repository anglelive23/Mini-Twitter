namespace Mini_Twitter.Application.Features.Users.Queries.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
