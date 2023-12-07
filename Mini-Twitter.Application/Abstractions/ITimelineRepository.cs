namespace Mini_Twitter.Application.Abstractions
{
    public interface ITimelineRepository
    {
        #region GET
        List<Tweet>? GetTimeLineForAUser(string userId, int pageNumer, int pageSize);
        #endregion
    }
}
