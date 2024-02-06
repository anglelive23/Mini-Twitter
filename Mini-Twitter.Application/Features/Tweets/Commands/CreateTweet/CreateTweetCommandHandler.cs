namespace Mini_Twitter.Application.Features.Tweets.Commands.CreateTweet
{
    public class CreateTweetCommandHandler : IRequestHandler<CreateTweetCommand, TweetDto>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public CreateTweetCommandHandler(ITweetRepository repo, IMediator mediator)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region Interface Implementation
        public async Task<TweetDto> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTweetCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkAdd = await _repo
                .AddTweetAsync(request.TweetDto.Adapt<Tweet>());

            await _mediator.Publish(new TweetCreatedNotification { Key = Constants.TweetsKey }, cancellationToken);

            return checkAdd.Adapt<TweetDto>();
        }
        #endregion
    }
}
