﻿namespace Mini_Twitter.API.Controllers
{
    [Route("api/odata")]
    [Authorize]
    public class ApplicationUsersController : BaseControllerModel
    {
        #region Constructors
        public ApplicationUsersController(IMediator mediator) : base(mediator) { }
        #endregion

        #region GET
        [HttpGet("applicationusers({key:guid})")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(string key, ODataQueryOptions<ApplicationUserDto> options)
        {
            var user = await _mediator
                .Send(new GetUserQuery
                {
                    UserId = key,
                    QueryOptions = options
                });

            if (user is null)
                return NotFound($"User with Id: {key} was not found!");
            return Ok(user);
        }

        [HttpGet("applicationusers({key:guid})/tweets")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTweetsForUser(string key, ODataQueryOptions<TweetDto> options)
        {
            var tweets = await _mediator
                .Send(new GetUserTweetsListQuery
                {
                    UserId = key,
                    QueryOptions = options
                });
            return Ok(tweets);
        }

        [HttpGet("applicationusers({key:guid})/tweets({tweetKey})")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTweetByIdForUser(string key, int tweetKey, ODataQueryOptions<TweetDto> options)
        {
            var tweet = await _mediator
                .Send(new GetUserTweetDetailsQuery
                {
                    UserId = key,
                    TweetId = tweetKey,
                    QueryOptions = options
                });
            return Ok(SingleResult.Create(tweet));
        }

        [HttpGet("applicationusers({key:guid})/retweets")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRetweetsForUser(string key, ODataQueryOptions<RetweetDto> options)
        {
            var retweets = await _mediator
                .Send(new GetUserRetweetsListQuery
                {
                    UserId = key,
                    QueryOptions = options
                });
            return Ok(retweets);
        }

        [HttpGet("applicationusers({key:guid})/retweets({retweetKey})")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRetweetByIdForUser(string key, int retweetKey, ODataQueryOptions<RetweetDto> options)
        {
            var retweet = await _mediator
                .Send(new GetUserRetweetDetailsQuery
                {
                    UserId = key,
                    RetweetId = retweetKey,
                    QueryOptions = options
                });
            return Ok(SingleResult.Create(retweet));
        }
        #endregion

        #region POST
        [HttpPost("applicationusers({key:guid})/retweets")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddRetweetForUser(string key, [FromBody] CreateRetweetDto retweetDto)
        {
            var retweet = await _mediator
                .Send(new CreateRetweetCommand
                {
                    UserId = key,
                    RetweetDto = retweetDto
                });

            if (retweet is null)
                return BadRequest($"Tweet or User not found on server!");
            return Created(retweet);
        }
        #endregion

        #region DELETE
        [HttpDelete("applicationusers({key:guid})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveUser(string key)
        {
            var currentUser = await _mediator
                .Send(new DeleteUserCommand
                {
                    UserId = key
                });

            if (currentUser is false)
                return NotFound("user not found!");

            return NoContent();
        }
        #endregion
    }
}
