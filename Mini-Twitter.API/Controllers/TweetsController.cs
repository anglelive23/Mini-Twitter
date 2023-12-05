namespace Mini_Twitter.Controllers
{
    [Route("api/odata")]
    [Authorize]
    public class TweetsController : ODataController
    {
        #region Fields and Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public TweetsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region GET
        [HttpGet("tweets")]
        [OutputCache(PolicyName = "Tweets")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<Tweet>))]
        public async Task<IActionResult> GetAllTweets()
        {
            var tweets = await _mediator
                .Send(new GetTweetsListQuery());
            return Ok(tweets);
        }

        [HttpGet("tweets({key})")]
        [OutputCache(PolicyName = "Tweet")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tweet))]
        public async Task<IActionResult> GetTweetById(int key)
        {
            var address = await _mediator
                .Send(new GetTweetDetailsQuery
                {
                    Id = key
                });

            return Ok(SingleResult.Create(address));
        }
        #endregion

        #region Post
        [HttpPost("tweets")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Tweet))]
        public async Task<IActionResult> AddTweet([FromBody] CreateTweetDto tweetDto, [FromServices] IOutputCacheStore cache, CancellationToken cancellationToken)
        {
            var tweet = await _mediator
                .Send(new CreateTweetCommand
                {
                    TweetDto = tweetDto
                });

            await cache.EvictByTagAsync("Tweets", cancellationToken);
            return Created(tweet);
        }
        #endregion
    }
}
