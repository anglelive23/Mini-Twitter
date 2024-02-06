//using Mini_Twitter.Application.Features.Tweets.Notifications;

//namespace Mini_Twitter.Application.Features.Tweets.Commands.PatchTweet
//{
//    public class PatchTweetCommandHandler : IRequestHandler<PatchTweetCommand, Tweet?>
//    {
//        #region Fields and Properties
//        private readonly ITweetRepository _repo;
//        private readonly IMediator _mediator;
//        #endregion

//        #region Constructors
//        public PatchTweetCommandHandler(ITweetRepository repo, IMediator mediator)
//        {
//            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
//            _mediator = mediator;
//        }
//        #endregion

//        #region Interface Implementation
//        public async Task<Tweet?> Handle(PatchTweetCommand request, CancellationToken cancellationToken)
//        {
//            var checkPatch = await _repo
//                .PartUpdateTweetAsync(request.Id, request.Delta);

//            await _mediator.Publish(new TweetCreatedNotification());

//            return checkPatch;
//        }
//        #endregion
//    }
//}
