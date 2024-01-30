namespace Mini_Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TimeLinesController : ControllerBase
    {
        #region Fields and Properties
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public TimeLinesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        #endregion

        #region GET
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetTimeLineForAUser(string userId, int pageNumber = 1, int pageSize = 1)
        {
            var timeline = await _mediator
                .Send(new GetTimeLineQuery
                {
                    UserId = userId,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });

            if (timeline == null)
                return NotFound();

            return Ok(timeline);
        }
        #endregion
    }
}
