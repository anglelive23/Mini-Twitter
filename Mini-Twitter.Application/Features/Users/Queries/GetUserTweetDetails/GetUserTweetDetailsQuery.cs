namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetDetails
{
    public class GetUserTweetDetailsQuery : IRequest<IQueryable<Tweet>>
    {
        public string UserId { get; set; }
        public int TweetId { get; set; }
    }
}
