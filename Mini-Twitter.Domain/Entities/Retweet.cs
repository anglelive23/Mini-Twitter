namespace Mini_Twitter.Domain.Entities
{
    public class Retweet
    {
        public int Id { get; set; }
        public ApplicationUser? User { get; set; }
        public string UserId { get; set; }
        public Tweet? Tweet { get; set; }
        public int TweetId { get; set; }
        [Contained]
        public ICollection<Reply>? Replies { get; set; }
    }
}
