using AutoMapper;
using AutoMapper.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Mini_Twitter.Application.Extensions;

namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetsList
{
    public class GetTweetsListQueryHandler : IRequestHandler<GetTweetsListQuery, IQueryable<TweetDto>>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDistributedCache _cache;
        #endregion

        #region Constructors
        public GetTweetsListQueryHandler(ITweetRepository repo, IMapper mapper, IHttpContextAccessor contextAccessor, IDistributedCache cache)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<TweetDto>> Handle(GetTweetsListQuery request, CancellationToken cancellationToken)
        {
            var key = $"tweets-{_contextAccessor.HttpContext.Request.QueryString}";
            var tweetsDto = await _cache.GetOrSetAsync(key, async token =>
            {
                var tweets = await _repo
                .GetAll(t => t.IsDeleted == false)
                .GetQueryAsync(_mapper, request.Options);
                return tweets;
            }, CacheOptions.DefaultExpiration
            , cancellationToken);
            return tweetsDto;
        }
        #endregion
    }
}
