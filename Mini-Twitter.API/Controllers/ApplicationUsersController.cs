using Mini_Twitter.Application.Features.Users.Commands.CreateRetweet;
using Mini_Twitter.Application.Features.Users.Queries.GetUserTweetsList;

namespace Mini_Twitter.Controllers
{
    [Route("api/odata")]
    public class ApplicationUsersController : ODataController
    {
        #region Fields and Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public ApplicationUsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region GET
        [HttpGet("applicationusers({key:guid})/tweets")]
        [EnableQuery(MaxExpansionDepth = 3, PageSize = 1000)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<Tweet>))]
        public async Task<IActionResult> GetTweetsForUser(string key)
        {
            var tweets = await _mediator
                .Send(new GetUserTweetsListQuery
                {
                    UserId = key
                });
            return Ok(tweets);
        }
        #endregion

        #region POST
        [HttpPost("applicationusers({key:guid})/retweets")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Retweet))]
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
    }
}
