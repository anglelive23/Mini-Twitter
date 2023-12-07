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

        public IQueryable<Retweet> GetRetweetsForUser(string userId)
        {
            try
            {
                if (!IsExistingUser(userId))
                    return Enumerable.Empty<Retweet>().AsQueryable();

                var retweets = _context
                    .Retweets
                    .Where(t => t.UserId == userId);

                return retweets;
            }
            catch (Exception ex) when (ex is ArgumentNullException
                                    || ex is InvalidOperationException
                                    || ex is PostgresException)
            {
                throw new DataFailureException(ex.Message);
            }
        }

        public IQueryable<Retweet> GetRetweetByIdForUser(string userId, int retweetId)
        {
            try
            {
                if (!IsExistingRetweetForAUser(retweetId, userId))
                    return Enumerable.Empty<Retweet>().AsQueryable();

                var retweets = _context
                    .Retweets
                    .Where(t => t.UserId == userId && t.Id == retweetId && t.IsDeleted == false);

                return retweets;
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

        #region DELETE
        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _userManger
                    .FindByIdAsync(userId);

                if (user == null)
                    return false;

                if (user.IsDeleted == true)
                    return true;

                user.IsDeleted = true;
                user.LastModifiedDate = DateTime.UtcNow;
                await _context.Tweets
                    .Where(r => r.UserId == userId)
                    .ExecuteUpdateAsync(u => u.SetProperty(p => p.IsDeleted, true));
                await _context.Retweets
                    .Where(r => r.UserId == userId)
                    .ExecuteUpdateAsync(u => u.SetProperty(p => p.IsDeleted, true));
                _context.Update(user);

                return _context.SaveChanges() > 0;
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
                .Any(u => u.Id == userId && u.IsDeleted == false);
        }

        private async Task<bool> IsExistingUserAsync(string userId)
        {
            return await _context
                .Users
                .AnyAsync(u => u.Id == userId && u.IsDeleted == false);
        }

        private async Task<bool> IsExistingTweet(int tweetId)
        {
            return await _context
                .Tweets
                .AnyAsync(t => t.Id == tweetId && t.IsDeleted == false);
        }

        private bool IsExistingRetweetForAUser(int retweetId, string userId)
        {
            return _context
                .Retweets
                .Any(t => t.Id == retweetId && t.UserId == userId && t.IsDeleted == false);
        }
        #endregion
    }
}
