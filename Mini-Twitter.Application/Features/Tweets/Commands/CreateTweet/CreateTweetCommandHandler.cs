namespace Mini_Twitter.Application.Features.Tweets.Commands.CreateTweet
{
    public class CreateTweetCommandHandler : IRequestHandler<CreateTweetCommand, Tweet>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public CreateTweetCommandHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<Tweet> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTweetCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkAdd = await _repo
                .AddTweetAsync(request.TweetDto.Adapt<Tweet>());
            return checkAdd;
        }
        #endregion
    }
}
