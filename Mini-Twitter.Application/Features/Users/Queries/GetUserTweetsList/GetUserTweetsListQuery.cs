namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetsList
{
    public class GetUserTweetsListQuery : IRequest<IQueryable<TweetDto>>
    {
        public string UserId { get; set; }
        public required ODataQueryOptions<TweetDto> QueryOptions { get; set; }
    }
}
