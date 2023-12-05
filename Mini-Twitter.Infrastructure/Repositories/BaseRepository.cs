namespace Mini_Twitter.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        #region Fields and Properties
        protected readonly TwitterContext _context;
        #endregion

        #region Constructors
        public BaseRepository(TwitterContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region GET
        public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null)
        {
            try
            {
                IQueryable<T> data = _context
                      .Set<T>();

                if (predicate is not null)
                    data = data.Where(predicate);

                return data;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context
                    .Set<T>().FindAsync(id);

                return entity;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }
        #endregion
    }
}
