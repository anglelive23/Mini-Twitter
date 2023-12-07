namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetsList
{
    public class GetUserTweetsListQuery : IRequest<IQueryable<Tweet>>
    {
        public string UserId { get; set; }
    }
}
