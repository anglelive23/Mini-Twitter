namespace Mini_Twitter.Application.Features.Follows.Commands.UnfollowCommand
{
    public class UnfollowCommand : IRequest<bool>
    {
        public FollowDto FollowDto { get; set; }
    }
}
