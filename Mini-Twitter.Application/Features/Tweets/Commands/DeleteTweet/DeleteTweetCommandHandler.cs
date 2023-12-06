namespace Mini_Twitter.Application.Features.Tweets.Commands.DeleteTweet
{
    public class DeleteTweetCommandHandler : IRequestHandler<DeleteTweetCommand, bool>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public DeleteTweetCommandHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<bool> Handle(DeleteTweetCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTweetCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkDelete = await _repo
                .RemoveTweetAsync(request.Id);
            return checkDelete;
        }
        #endregion
    }
}
