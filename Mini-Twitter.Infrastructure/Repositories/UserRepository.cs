namespace Mini_Twitter.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        #region Fields and Properties
        private readonly UserManager<ApplicationUser> _userManger;
        #endregion

        #region Constructors
        public UserRepository(TwitterContext context, UserManager<ApplicationUser> userManger) : base(context)
        {
            _userManger = userManger;
        }
        #endregion

        #region GET
        public IQueryable<Tweet> GetTweetsForUser(string userId)
        {
            try
            {
                if (!IsExistingUser(userId))
                    return Enumerable.Empty<Tweet>().AsQueryable();

                var tweets = _context
                    .Tweets
                    .Where(t => t.UserId == userId);

                return tweets;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }
        #endregion

        #region POST
        public async Task<Retweet?> AddRetweetForUserAsync(string userId, int tweetId)
        {
            try
            {
                if (!await IsExistingUserAsync(userId))
                    return null;

                if (!await IsExistingTweet(tweetId))
                    return null;

                var retweet = new Retweet { TweetId = tweetId, UserId = userId };

                _context.Retweets.Add(retweet);
                await _context.SaveChangesAsync();

                return retweet;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is DbUpdateException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }
        #endregion

        #region Helper methods
        private bool IsExistingUser(string userId)
        {
            return _context
                .Users
                .Any(u => u.Id == userId);
        }

        private async Task<bool> IsExistingUserAsync(string userId)
        {
            return await _context
                .Users
                .AnyAsync(u => u.Id == userId);
        }

        private async Task<bool> IsExistingTweet(int tweetId)
        {
            return await _context
                .Tweets
                .AnyAsync(t => t.Id == tweetId);
        }
        #endregion
    }
}
