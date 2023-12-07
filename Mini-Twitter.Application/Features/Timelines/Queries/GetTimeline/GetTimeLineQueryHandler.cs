namespace Mini_Twitter.Application.Features.Timelines.Queries.GetTimeline
{
    public class GetTimeLineQueryHandler : IRequestHandler<GetTimeLineQuery, List<Tweet>?>
    {
        #region Fields and Properties
        private readonly ITimelineRepository _repo;
        #endregion

        #region Constructors
        public GetTimeLineQueryHandler(ITimelineRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<List<Tweet>?> Handle(GetTimeLineQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTimeLineQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var timeline = _repo
                .GetTimeLineForAUser(request.UserId, request.PageNumber, request.PageSize);
            return timeline;
        }
        #endregion
    }
}
