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
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTweets(ODataQueryOptions<TweetDto> options)
        {
            var tweets = await _mediator
                    .Send(new GetTweetsListQuery() { Options = options });
            return Ok(tweets);
        }

        [HttpGet("tweets({key})")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTweetById(int key, ODataQueryOptions<TweetDto> options)
        {
            var tweet = await _mediator
                .Send(new GetTweetDetailsQuery
                {
                    Id = key,
                    Options = options
                });

            if (tweet is null)
                return NotFound($"Tweet with Id: {key} was not found!");

            return Ok(tweet);
        }
        #endregion

        #region Post
        [HttpPost("tweets")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddTweet([FromBody] CreateTweetDto tweetDto)
        {
            var tweet = await _mediator
                .Send(new CreateTweetCommand
                {
                    TweetDto = tweetDto
                });

            if (tweet is null)
                return BadRequest("Tweet not created!");

            return Created(tweet);
        }

        [HttpPost("tweets({key})/replies")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddReplyForTweet(int key, [FromBody] CreateReplyDto replyDto)
        {
            var reply = await _mediator
                .Send(new CreateTweetReplyCommand
                {
                    Id = key,
                    ReplyDto = replyDto
                });

            if (reply is null)
                return BadRequest($"Tweet with id: {key} does not exist on the server!");

            return Created(reply);
        }
        #endregion

        #region PUT
        [HttpPut("tweets({key})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTweet(int key, [FromBody] UpdateTweetDto updateTweetDto)
        {
            var currentTweet = await _mediator
                .Send(new UpdateTweetCommand
                {
                    Id = key,
                    UpdateTweetDto = updateTweetDto
                });

            if (currentTweet == null)
                return NotFound("Tweet not found!");

            return NoContent();
        }

        [HttpPut("tweets({key})/replies({replyId})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReplyForTweet(int key, int replyId, [FromBody] UpdateTweetReplyDto replyDto)
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

            return NoContent();
        }
        #endregion

        #region PATCH
        // Todo: Fix Delta<Tweet> bug, somehow it broke.
        //[HttpPatch("tweets({key})")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> PartUpdateTweet(int key, Delta<Tweet> delta)
        //{
        //    var currentTweet = await _mediator
        //        .Send(new PatchTweetCommand
        //        {
        //            Id = key,
        //            Delta = delta.Adapt<Delta<Tweet>>(),
        //        });

        //    if (currentTweet is null)
        //        return NotFound("Tweet not found!");

        //    return NoContent();
        //}
        #endregion

        #region DELETE
        [HttpDelete("tweets({key})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveTweet(int key)
        {
            var currentTweet = await _mediator
                .Send(new DeleteTweetCommand
                {
                    Id = key
                });

            if (currentTweet is false)
                return NotFound("Tweet not found!");

            return NoContent();
        }

        [HttpDelete("tweets({key})/replies({replyId})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveReplyForTweet(int key, int replyId)
        {
            var currentReply = await _mediator
                .Send(new DeleteTweetReplyCommand
                {
                    TweetId = key,
                    ReplyId = replyId,
                });

            if (currentReply is false)
                return NotFound("Tweet or Reply not found!");

            return NoContent();
        }
        #endregion
    }
}
