namespace Mini_Twitter.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly TwitterContext _context;

        public ModuleRepository(TwitterContext context) => _context = context;

        public async Task<Module> AddModuleAsync(Module module)
        {
            try
            {
                module.CreatedDate = DateTime.UtcNow;
                module.LastModifiedDate = DateTime.UtcNow;
                module.IsDeleted = false;

                _context.Modules.Add(module);
                await _context.SaveChangesAsync();

                return module;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is DbUpdateException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }
    }
}
