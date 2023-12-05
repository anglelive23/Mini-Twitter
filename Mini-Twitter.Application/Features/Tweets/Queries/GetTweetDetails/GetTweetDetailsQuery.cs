namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetDetails
{
    public class GetTweetDetailsQuery : IRequest<IQueryable<Tweet>>
    {
        public int Id { get; set; }
    }
}
