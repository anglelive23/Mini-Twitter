namespace Mini_Twitter.Application.Abstractions
{
    public interface IModuleRepository
    {
        Task<Module> AddModuleAsync(Module module);
    }
}
