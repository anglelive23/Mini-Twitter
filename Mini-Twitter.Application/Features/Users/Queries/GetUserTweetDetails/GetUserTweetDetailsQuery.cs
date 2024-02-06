namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetDetails
{
    public class GetUserTweetDetailsQuery : IRequest<IQueryable<TweetDto>>
    {
        public string UserId { get; set; }
        public int TweetId { get; set; }
        public required ODataQueryOptions<TweetDto> QueryOptions { get; set; }
    }
}
