namespace Mini_Twitter.Application.Features.Follows.Commands.UnfollowCommand
{
    public class UnfollowCommandValidator : AbstractValidator<UnfollowCommand>
    {
        public UnfollowCommandValidator()
        {
            RuleFor(l => l.FollowDto.FollowerId)
                .NotEmpty().WithMessage("{PropertyName} is required!");

            RuleFor(l => l.FollowDto.FolloweeId)
                .NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
