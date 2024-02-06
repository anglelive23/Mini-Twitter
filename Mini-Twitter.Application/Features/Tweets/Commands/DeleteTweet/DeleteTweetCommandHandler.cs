namespace Mini_Twitter.Application.Features.Tweets.Commands.DeleteTweet
{
    public class DeleteTweetCommandHandler : IRequestHandler<DeleteTweetCommand, bool>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public DeleteTweetCommandHandler(ITweetRepository repo, IMediator mediator)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region Interface Implementation
        public async Task<bool> Handle(DeleteTweetCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTweetCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkDelete = await _repo
                .RemoveTweetAsync(request.Id);

            if (checkDelete)
                await _mediator.Publish(new TweetDeletedNotification { Key = $"{Constants.TweetsKey}" }, cancellationToken);

            return checkDelete;
        }
        #endregion
    }
}
