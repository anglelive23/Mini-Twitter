namespace Mini_Twitter.Application.Features.Modules.Commands.CreateModule
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, CreateModuleRequestDto>
    {
        private readonly IModuleRepository _moduleRepository;

        public CreateModuleCommandHandler(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository ?? throw new ArgumentNullException(nameof(moduleRepository));
        }

        public async Task<CreateModuleRequestDto> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateModuleCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkAdd = await _moduleRepository.AddModuleAsync(request.ModuleDto.Adapt<Module>());
            return checkAdd.Adapt<CreateModuleRequestDto>();
        }
    }
}
