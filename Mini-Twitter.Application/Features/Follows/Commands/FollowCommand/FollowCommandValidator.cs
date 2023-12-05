namespace Mini_Twitter.Application.Features.Follows.Commands.FollowCommand
{
    public class FollowCommandValidator : AbstractValidator<FollowCommand>
    {
        public FollowCommandValidator()
        {
            RuleFor(l => l.FollowDto.FollowerId)
                .NotEmpty().WithMessage("{PropertyName} is required!");

            RuleFor(l => l.FollowDto.FolloweeId)
                .NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
