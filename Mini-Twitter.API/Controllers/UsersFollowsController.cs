namespace Mini_Twitter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersFollowsController : ControllerBase
    {
        #region Fields and Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public UsersFollowsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region Follow & Unfollow
        [HttpPost("follow")]
        public async Task<IActionResult> FollowUser(string followerId, string followeeId)
        {
            var followDto = new FollowDto { FollowerId = followerId, FolloweeId = followeeId };
            var followCommand = await _mediator.Send(
                new FollowCommand
                {
                    FollowDto = followDto
                });
            if (followCommand is false)
                return BadRequest("Something went wrong!");
            return Ok("Follow process completed");
        }

        [HttpPost("unfollow")]
        public async Task<IActionResult> UnfollowUser(string followerId, string followeeId)
        {
            var followDto = new FollowDto { FollowerId = followerId, FolloweeId = followeeId };
            var unfollowCommand = await _mediator.Send(
                new UnfollowCommand
                {
                    FollowDto = followDto
                });
            if (unfollowCommand is false)
                return BadRequest("Something went wrong!");
            return Ok("Unfollow process completed");
        }
        #endregion
    }
}
