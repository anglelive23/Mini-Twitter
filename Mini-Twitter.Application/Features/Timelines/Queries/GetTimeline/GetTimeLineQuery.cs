﻿namespace Mini_Twitter.Application.Features.Timelines.Queries.GetTimeline
{
    public class GetTimeLineQuery : IRequest<List<TweetDto>?>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
