using Microsoft.AspNetCore.OData.Query;

namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetDetails
{
    public class GetTweetDetailsQuery : IRequest<TweetDto?>
    {
        public int Id { get; set; }
        public ODataQueryOptions<TweetDto> Options { get; set; }
    }
}
