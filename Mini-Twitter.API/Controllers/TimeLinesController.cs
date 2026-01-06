namespace Mini_Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TimeLinesController : BaseControllerModel
    {

        #region Constructors
        public TimeLinesController(IMediator mediator) : base(mediator) { }
        #endregion

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetTimeLineForAUser(string userId, int pageNumber = 1, int pageSize = 1)
        {
            var result = await _mediator
                .Send(new GetTimeLineQuery
                {
                    UserId = userId,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });

            if (result.IsFailure)
                return NotFound(result);

            return Ok(result);
        }
        #endregion
    }
}
