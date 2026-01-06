namespace Mini_Twitter.Application.Abstractions
{
    public interface ITimelineRepository
    {
        #region GET
        Result<PaginatedResult<TweetDto>> GetTimeLineForAUser(string userId, int pageNumer, int pageSize);
        #endregion
    }
}
