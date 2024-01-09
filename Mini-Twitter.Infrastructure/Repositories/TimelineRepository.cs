namespace Mini_Twitter.Infrastructure.Repositories
{
    public class TimelineRepository : ITimelineRepository
    {
        #region Fields and Properties
        private readonly TwitterContext _context;
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public TimelineRepository(TwitterContext context, IUserService userService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        #endregion

        #region Interface Implementation
        public List<Tweet>? GetTimeLineForAUser(string userId, int pageNumer, int pageSize)
        {
            if (!_userService.IsExistingUser(userId))
                return null;

            var listOfFollowers = _userService.GetFollowersList(userId);

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
    }
}
