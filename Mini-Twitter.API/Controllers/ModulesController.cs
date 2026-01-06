
using Mini_Twitter.Application.Features.Modules.Commands.CreateModule;

namespace Mini_Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ModulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ModulesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        // todo: add full CRUD operations for modules
        [HttpPost("modules")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddModule([FromBody] CreateModuleRequestDto moduleDto)
        {
            var module = await _mediator
                .Send(new CreateModuleCommand
                {
                    ModuleDto = moduleDto
                });

            if (module is null)
                return BadRequest("Module not created!");

            return Ok(module);
        }
    }
}
