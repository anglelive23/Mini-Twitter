using Mini_Twitter.Application.Common;

namespace Mini_Twitter.Application.Features.Timelines.Queries.GetTimeline
{
    public class GetTimeLineQuery : IRequest<Result<PaginatedResult<TweetDto>>>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
