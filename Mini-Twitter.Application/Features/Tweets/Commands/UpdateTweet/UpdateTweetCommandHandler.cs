namespace Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweet
{
    public class UpdateTweetCommandHandler : IRequestHandler<UpdateTweetCommand, TweetDto?>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public UpdateTweetCommandHandler(ITweetRepository repo, IMediator mediator)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region Interface Implementation
        public async Task<TweetDto?> Handle(UpdateTweetCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTweetCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkUpdate = await _repo
                .UpdateTweetAsync(request.Id, request.UpdateTweetDto.Adapt<Tweet>());
            if (checkUpdate != null)
                await _mediator.Publish(new TweetUpdatedNotification { Key = $"{Constants.TweetKey}" }, cancellationToken);
            return checkUpdate.Adapt<TweetDto>();
        }
        #endregion
    }
}
