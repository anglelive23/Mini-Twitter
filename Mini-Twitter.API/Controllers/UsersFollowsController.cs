namespace Mini_Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersFollowsController : BaseControllerModel
    {
        #region Constructors
        public UsersFollowsController(IMediator mediator) : base(mediator) { }
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
