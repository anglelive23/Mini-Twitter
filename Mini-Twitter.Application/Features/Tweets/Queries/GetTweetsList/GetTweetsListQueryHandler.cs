namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetsList
{
    public class GetTweetsListQueryHandler : IRequestHandler<GetTweetsListQuery, List<TweetDto>>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        #endregion

        #region Constructors
        public GetTweetsListQueryHandler(ITweetRepository repo, IMapper mapper, ICacheService cacheService)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }
        #endregion

        #region Interface Implementation
        public async Task<List<TweetDto>> Handle(GetTweetsListQuery request, CancellationToken cancellationToken)
        {
            var tweetsDto = await _cacheService.GetAsync(null, async () =>
            {
                var tweets = await _repo
                        .GetAll(t => t.IsDeleted == false)
                        .GetQueryAsync(_mapper, request.Options);
                return tweets.ToList();
            }, CacheOptions.DefaultExpiration
            , cancellationToken);

            return tweetsDto;
        }
        #endregion
    }
}
