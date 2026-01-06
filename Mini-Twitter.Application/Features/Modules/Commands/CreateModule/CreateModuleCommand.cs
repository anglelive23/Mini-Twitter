namespace Mini_Twitter.Application.Features.Modules.Commands.CreateModule
{
    public class CreateModuleCommand : IRequest<CreateModuleRequestDto>
    {
        public CreateModuleRequestDto ModuleDto { get; set; }
    }
}
