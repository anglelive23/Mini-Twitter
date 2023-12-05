namespace Mini_Twitter.Application.Features.Authentications.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MaximumLength(256).WithMessage("{PropertyName} max length is 256!")
                .EmailAddress().WithMessage("{PropertyName} must be email address!");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
