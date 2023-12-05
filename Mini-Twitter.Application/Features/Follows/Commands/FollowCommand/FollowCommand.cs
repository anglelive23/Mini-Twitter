namespace Mini_Twitter.Application.Features.Follows.Commands.FollowCommand
{
    public class FollowCommand : IRequest<bool>
    {
        public FollowDto FollowDto { get; set; }
    }
}
