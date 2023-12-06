namespace Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweet
{
    public class UpdateTweetCommandHandler : IRequestHandler<UpdateTweetCommand, Tweet?>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public UpdateTweetCommandHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<Tweet?> Handle(UpdateTweetCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTweetCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkUpdate = await _repo
                .UpdateTweetAsync(request.Id, request.UpdateTweetDto.Adapt<Tweet>());
            return checkUpdate;
        }
        #endregion
    }
}
