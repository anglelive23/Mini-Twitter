namespace Mini_Twitter.Application.Features.Modules.Commands.CreateModule
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(l => l.ModuleDto.Name)
                .NotEmpty().WithMessage("Module name is required.")
                .MaximumLength(100).WithMessage("Module name must not exceed 100 characters.");

            RuleFor(l => l.ModuleDto.Description)
                .NotEmpty().WithMessage("Module description is required.")
                .MaximumLength(500).WithMessage("Module description must not exceed 500 characters.");
        }
    }
}
