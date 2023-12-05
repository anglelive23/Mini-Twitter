namespace Mini_Twitter.Application.Models.Dtos
{
    public class FollowDto
    {
        public string FolloweeId { get; set; } // the one i will follow
        public string FollowerId { get; set; } // me
    }
}
