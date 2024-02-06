namespace Mini_Twitter.Application.Models.Dtos
{
    public class RetweetDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TweetId { get; set; }
        [Contained]
        public ICollection<ReplyDto>? Replies { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
