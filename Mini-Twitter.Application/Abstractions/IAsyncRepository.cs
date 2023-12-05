namespace Mini_Twitter.Application.Abstractions
{
    public interface IAsyncRepository<T> where T : class
    {
        #region GET
        IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null);
        Task<T?> GetByIdAsync(int id);
        #endregion
    }
}
