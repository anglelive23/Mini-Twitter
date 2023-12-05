namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetsList
{
    public class GetTweetsListQueryHandler : IRequestHandler<GetTweetsListQuery, IQueryable<Tweet>>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public GetTweetsListQueryHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<Tweet>> Handle(GetTweetsListQuery request, CancellationToken cancellationToken)
        {
            var tweets = _repo.GetAll();
            return await Task.FromResult(tweets);
        }
        #endregion
    }
}
