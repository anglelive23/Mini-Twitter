namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetsList
{
    public class GetUserRetweetsListQuery : IRequest<IQueryable<Retweet>>
    {
        public string UserId { get; set; }
    }
}
