namespace Mini_Twitter.Domain.Entities
{
    public class UserTweet : AuditableEntity
    {
        public int Id { get; set; }
        public ApplicationUser? User { get; set; }
        public string UserId { get; set; }

        public Tweet? Tweet { get; set; }
        public int TweetId { get; set; }
    }
}
