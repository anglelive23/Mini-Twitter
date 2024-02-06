namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetsList
{
    public class GetUserTweetsListQueryHandler : IRequestHandler<GetUserTweetsListQuery, IQueryable<TweetDto>>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        #endregion

        #region Constuctors
        public GetUserTweetsListQueryHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<TweetDto>> Handle(GetUserTweetsListQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserTweetsListQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var tweets = _repo
                .GetTweetsForUser(request.UserId)
                .GetQuery(_mapper, request.QueryOptions);
            return tweets;
        }
        #endregion
    }
}
