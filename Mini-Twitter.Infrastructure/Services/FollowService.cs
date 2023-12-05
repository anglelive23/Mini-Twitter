namespace Mini_Twitter.Infrastructure.Services
{
    public class FollowService : IFollowService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public FollowService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<bool> FollowUserAsync(string followerId, string followeeId)
        {
            var follower = await _userManager
                .FindByIdAsync(followerId); // me

            var followee = await _userManager
                .FindByIdAsync(followeeId); // other person

            if (follower != null && followee != null)
            {
                follower.Followees.Add(followee);
                followee.Followers.Add(follower);

                followee.FollowersCount += 1;
                follower.FollowingCount += 1;
                await _userManager.UpdateAsync(follower);
                await _userManager.UpdateAsync(followee);

                return true;
            }

            return false;
        }

        public async Task<bool> UnfollowUserAsync(string followerId, string followeeId)
        {
            var follower = await _userManager
                .FindByIdAsync(followerId);

            var followee = await _userManager
                .FindByIdAsync(followeeId);

            if (follower != null && followee != null)
            {
                follower.Followees.Remove(followee);
                followee.Followers.Remove(follower);


                followee.FollowersCount -= 1;
                follower.FollowingCount -= 1;
                await _userManager.UpdateAsync(follower);
                await _userManager.UpdateAsync(followee);

                return true;
            }

            return false;
        }
    }
}
