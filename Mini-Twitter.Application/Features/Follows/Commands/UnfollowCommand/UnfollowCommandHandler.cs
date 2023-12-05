namespace Mini_Twitter.Application.Features.Follows.Commands.UnfollowCommand
{
    public class UnfollowCommandHandler : IRequestHandler<UnfollowCommand, bool>
    {
        #region Fields and Properties
        private readonly IFollowService _followService;
        #endregion

        #region Constructors
        public UnfollowCommandHandler(IFollowService followService)
        {
            this._followService = followService ?? throw new ArgumentNullException(nameof(followService));
        }
        #endregion

        #region Interface Implementation
        public async Task<bool> Handle(UnfollowCommand request, CancellationToken cancellationToken)
        {
            var validator = new UnfollowCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkUnfollow = await _followService
                .UnfollowUserAsync(request.FollowDto.FollowerId, request.FollowDto.FolloweeId);
            return checkUnfollow;
        }
        #endregion
    }
}
