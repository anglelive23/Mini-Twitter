namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetDetails
{
    public class GetUserRetweetDetailsQuery : IRequest<IQueryable<Retweet>>
    {
        public string UserId { get; set; }
        public int RetweetId { get; set; }
    }
}
