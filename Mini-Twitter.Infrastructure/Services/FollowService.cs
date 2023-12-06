namespace Mini_Twitter.Infrastructure.Services
{
    public class FollowService : IFollowService
    {
        #region Fields and Properties
        private readonly TwitterContext _context;
        #endregion

        #region Constructors
        public FollowService(TwitterContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Follow & Unfollow
        public async Task<bool> FollowUserAsync(string followerId, string followeeId)
        {
            var follower = await GetUserByIdAsync(followerId);
            var followee = await GetUserByIdAsync(followeeId);

            if (follower is null || followee is null || await FollowRelationshipExistsAsync(followerId, followee))
                return false;

            follower.Followees.Add(followee);
            follower.FollowingCount += 1;
            followee.FollowersCount += 1;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnfollowUserAsync(string followerId, string followeeId)
        {
            var follower = await GetUserByIdAsync(followerId);
            var followee = await GetUserByIdAsync(followeeId);

            if (follower is null || followee is null || !await FollowRelationshipExistsAsync(followerId, followee))
                return false;

            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM security.\"UserFollowers\" WHERE \"FolloweeId\" = {0} AND \"FollowerId\" = {1}",
                followeeId, followerId);

            followee.FollowersCount -= 1;
            follower.FollowingCount -= 1;
            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

        #region Helper methods
        private async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        private async Task<bool> FollowRelationshipExistsAsync(string followerId, ApplicationUser followee)
        {
            return await _context
                .Users
                .AnyAsync(f => f.Id == followerId && f.Followees.Contains(followee));
        }
        #endregion
    }
}
