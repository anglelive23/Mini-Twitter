namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetDetails
{
    public class GetTweetDetailsQueryHandler : IRequestHandler<GetTweetDetailsQuery, TweetDto?>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        #endregion

        #region Constructors
        public GetTweetDetailsQueryHandler(ITweetRepository repo, IMapper mapper, ICacheService cacheService)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }
        #endregion

        #region Interface Implementation
        public async Task<TweetDto?> Handle(GetTweetDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTweetDetailsQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var tweetDto = await _cacheService
                .GetAsync(null, async () =>
                {
                    var tweet = await _repo
                        .GetAll(r => r.Id == request.Id && r.IsDeleted == false)
                        .GetQueryAsync(_mapper, request.Options);

                    return tweet.FirstOrDefault()!;
                }, CacheOptions.DefaultExpiration
                , cancellationToken);
            return tweetDto;
        }
        #endregion
    }
}
