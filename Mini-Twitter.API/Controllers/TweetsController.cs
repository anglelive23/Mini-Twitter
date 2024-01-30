namespace Mini_Twitter.API.Controllers
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTweets()
        {
            var tweets = await _mediator
                .Send(new GetTweetsListQuery());
            return Ok(tweets);
        }

        [HttpGet("tweets({key})")]
        [OutputCache(PolicyName = "Tweet")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
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

        [HttpPost("tweets({key})/replies")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddReplyForTweet(int key, [FromBody] CreateReplyDto replyDto, [FromServices] IOutputCacheStore cache, CancellationToken cancellationToken)
        {
            var reply = await _mediator
                .Send(new CreateTweetReplyCommand
                {
                    Id = key,
                    ReplyDto = replyDto
                });

            if (reply is null)
                return BadRequest($"Tweet with id: {key} does not exist on the server!");

            await cache.EvictByTagAsync("Tweets", cancellationToken);

            return Created(reply);
        }
        #endregion

        #region PUT
        [HttpPut("tweets({key})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTweet(int key, [FromBody] UpdateTweetDto updateTweetDto, [FromServices] IOutputCacheStore cache, CancellationToken cancellationToken)
        {
            var currentTweet = await _mediator
                .Send(new UpdateTweetCommand
                {
                    Id = key,
                    UpdateTweetDto = updateTweetDto
                });

            if (currentTweet == null)
                return NotFound("Tweet not found!");

            await cache.EvictByTagAsync("Tweets", cancellationToken);

            return NoContent();
        }

        [HttpPut("tweets({key})/replies({replyId})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReplyForTweet(int key, int replyId, [FromBody] UpdateTweetReplyDto replyDto, [FromServices] IOutputCacheStore cache, CancellationToken cancellationToken)
        {
            var currentReply = await _mediator
                .Send(new UpdateTweetReplyCommand
                {
                    TweetId = key,
                    ReplyId = replyId,
                    UpdateTweetReplyDto = replyDto
                });

            if (currentReply == null)
                return NotFound("Tweet or Reply not found!");

            await cache.EvictByTagAsync("Tweets", cancellationToken);

            return NoContent();
        }
        #endregion

        #region PATCH
        [HttpPatch("tweets({key})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PartUpdateTweet(int key, Delta<Domain.Entities.Tweet> delta, [FromServices] IOutputCacheStore cache, CancellationToken cancellationToken)
        {
            var currentTweet = await _mediator
                .Send(new PatchTweetCommand
                {
                    Id = key,
                    Delta = delta,
                });

            if (currentTweet is null)
                return NotFound("Tweet not found!");

            await cache.EvictByTagAsync("Tweets", cancellationToken);
            return NoContent();
        }
        #endregion

        #region DELETE
        [HttpDelete("tweets({key})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveTweet(int key, [FromServices] IOutputCacheStore cache, CancellationToken cancellationToken)
        {
            var currentTweet = await _mediator
                .Send(new DeleteTweetCommand
                {
                    Id = key
                });

            if (currentTweet is false)
                return NotFound("Tweet not found!");

            await cache.EvictByTagAsync("Tweets", cancellationToken);
            return NoContent();
        }

        [HttpDelete("tweets({key})/replies({replyId})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveReplyForTweet(int key, int replyId, [FromServices] IOutputCacheStore cache, CancellationToken cancellationToken)
        {
            var currentReply = await _mediator
                .Send(new DeleteTweetReplyCommand
                {
                    TweetId = key,
                    ReplyId = replyId,
                });

            if (currentReply is false)
                return NotFound("Tweet or Reply not found!");

            await cache.EvictByTagAsync("Tweets", cancellationToken);

            return NoContent();
        }
        #endregion
    }
}
