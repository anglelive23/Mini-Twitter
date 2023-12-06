namespace Mini_Twitter.Domain.Entities
{
    public class Reply : AuditableEntity
    {
        public int Id { get; set; }
        public string Context { get; set; }
        public ApplicationUser? User { get; set; }
        public string UserId { get; set; }
        public Tweet? Tweet { get; set; }
        public int TweetId { get; set; }
    }
}
