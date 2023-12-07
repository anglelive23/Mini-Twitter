namespace Mini_Twitter.Infrastructure.Repositories
{
    public class TimelineRepository : ITimelineRepository
    {
        #region Fields and Properties
        private readonly TwitterContext _context;
        #endregion

        #region Constructors
        public TimelineRepository(TwitterContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Interface Implementation
        public List<Tweet>? GetTimeLineForAUser(string userId, int pageNumer, int pageSize)
        {
            if (!IsExistingUser(userId))
                return null;

            var listOfFollowers = GetFollowersList(userId);

            var timeline = _context
                .Tweets
                .Where(t => t.UserId == userId || listOfFollowers.Contains(t.UserId))
                .OrderByDescending(t => t.CreatedDate)
                .Skip((pageNumer - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return timeline;
        }
        #endregion

        #region Helper methods
        private List<string> GetFollowersList(string userId)
        {
            return _context
                .UserFollowers
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FolloweeId)
                .ToList();
        }

        private bool IsExistingUser(string userId)
        {
            return _context
                .Users
                .Any(u => u.Id == userId && u.IsDeleted == false);
        }
        #endregion
    }
}
