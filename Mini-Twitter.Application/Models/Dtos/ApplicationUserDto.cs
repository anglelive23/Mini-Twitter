namespace Mini_Twitter.Application.Models.Dtos
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        [Contained]
        public ICollection<TweetDto>? Tweets { get; set; }
        [Contained]
        public ICollection<RetweetDto>? Retweets { get; set; }
    }
}
