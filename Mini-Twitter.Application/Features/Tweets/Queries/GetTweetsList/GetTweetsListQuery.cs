using Microsoft.AspNetCore.OData.Query;

namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetsList
{
    public class GetTweetsListQuery : IRequest<List<TweetDto>>
    {
        public ODataQueryOptions<TweetDto> Options { get; set; }
    }
}
