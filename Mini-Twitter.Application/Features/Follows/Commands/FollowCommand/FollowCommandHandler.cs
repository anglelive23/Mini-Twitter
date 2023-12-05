namespace Mini_Twitter.Application.Features.Follows.Commands.FollowCommand
{
    public class FollowCommandHandler : IRequestHandler<FollowCommand, bool>
    {
        #region Fields and Properties
        private readonly IFollowService _followService;
        #endregion

        #region Constructors
        public FollowCommandHandler(IFollowService followService)
        {
            this._followService = followService ?? throw new ArgumentNullException(nameof(followService));
        }
        #endregion

        #region Interface Implementation
        public async Task<bool> Handle(FollowCommand request, CancellationToken cancellationToken)
        {
            var validator = new FollowCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkFollow = await _followService
                .FollowUserAsync(request.FollowDto.FollowerId, request.FollowDto.FolloweeId);
            return checkFollow;
        }
        #endregion
    }
}
