namespace Mini_Twitter.Application.Features.Tweets.Queries.GetTweetDetails
{
    public class GetTweetDetailsQueryHandler : IRequestHandler<GetTweetDetailsQuery, IQueryable<Tweet>>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public GetTweetDetailsQueryHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<Tweet>> Handle(GetTweetDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTweetDetailsQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var tweet = _repo
                .GetAll(r => r.Id == request.Id && r.IsDeleted == false);
            return tweet;
        }
        #endregion
    }
}
