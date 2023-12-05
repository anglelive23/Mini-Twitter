namespace Mini_Twitter.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public IList<Tweet>? Tweets { get; set; }

        public ICollection<ApplicationUser> Followers { get; set; }
        public ICollection<ApplicationUser> Followees { get; set; }
        public IList<RefreshToken>? RefreshTokens { get; set; }

        public ApplicationUser()
        {
            Followers = new List<ApplicationUser>();
            Followees = new List<ApplicationUser>();
            Tweets = new List<Tweet>();
            RefreshTokens = new List<RefreshToken>();
        }
    }
}
