using AutoMapper;
using AutoMapper.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Mini_Twitter.Application.Extensions;

namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetDetails
{
    public class GetTweetDetailsQueryHandler : IRequestHandler<GetTweetDetailsQuery, IQueryable<TweetDto>>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDistributedCache _cache;
        #endregion

        #region Constructors
        public GetTweetDetailsQueryHandler(ITweetRepository repo, IMapper mapper, IHttpContextAccessor contextAccessor, IDistributedCache cache)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<TweetDto>> Handle(GetTweetDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTweetDetailsQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var queryString = _contextAccessor.HttpContext!.Request.QueryString.ToString();
            var key = string.IsNullOrEmpty(queryString)
                ? Constants.TweetKey
                : $"{Constants.TweetKey}-{queryString}";

            var tweetDto = await _cache
                .GetOrSetAsync(key, async token =>
                {
                    var tweet = await _repo
                        .GetAll(r => r.Id == request.Id && r.IsDeleted == false)
                        .GetQueryAsync(_mapper, request.Options);
                    return tweet;
                }, CacheOptions.DefaultExpiration
                , cancellationToken);
            return tweetDto;
        }
        #endregion
    }
}
