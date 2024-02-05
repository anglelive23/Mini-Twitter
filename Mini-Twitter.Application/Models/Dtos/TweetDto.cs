namespace Mini_Twitter.Application.Models.Dtos
{
    public class TweetDto
    {
        public int Id { get; set; }
        public ApplicationUser? User { get; set; }
        public string UserId { get; set; }
        public string Context { get; set; }
        public int LikesCount { get; set; }
        public int RetweetCount { get; set; }
        [Contained]
        public ICollection<Reply>? Replies { get; set; }
        [Contained]
        public ICollection<Retweet>? Retweets { get; set; }
    }
}
