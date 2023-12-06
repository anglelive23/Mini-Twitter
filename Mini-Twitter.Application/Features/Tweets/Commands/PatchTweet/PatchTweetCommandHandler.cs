namespace Mini_Twitter.Application.Features.Tweets.Commands.PatchTweet
{
    public class PatchTweetCommandHandler : IRequestHandler<PatchTweetCommand, Tweet?>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public PatchTweetCommandHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<Tweet?> Handle(PatchTweetCommand request, CancellationToken cancellationToken)
        {
            var checkPatch = await _repo
                .PartUpdateTweetAsync(request.Id, request.Delta);
            return checkPatch;
        }
        #endregion
    }
}
