﻿namespace Mini_Twitter.Application.Models.Dtos
{
    public class TweetDto
    {
        public int Id { get; set; }
        public ApplicationUserDto? User { get; set; }
        public string UserId { get; set; }
        public string Context { get; set; }
        public int LikesCount { get; set; }
        public int RetweetCount { get; set; }
        [Contained]
        public ICollection<ReplyDto>? Replies { get; set; }
        [Contained]
        public ICollection<RetweetDto>? Retweets { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
