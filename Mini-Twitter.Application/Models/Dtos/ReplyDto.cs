namespace Mini_Twitter.Application.Models.Dtos
{
    public class ReplyDto
    {
        public int Id { get; set; }
        public string Context { get; set; }
        public string UserId { get; set; }
        public int? TweetId { get; set; }
        public int? RetweetId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
