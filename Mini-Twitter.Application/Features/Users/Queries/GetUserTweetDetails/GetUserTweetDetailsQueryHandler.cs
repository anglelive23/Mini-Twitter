namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetDetails
{
    public class GetUserTweetDetailsQueryHandler : IRequestHandler<GetUserTweetDetailsQuery, IQueryable<TweetDto>>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GetUserTweetDetailsQueryHandler(ITweetRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<TweetDto>> Handle(GetUserTweetDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserTweetDetailsQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var tweet = _repo
                .GetAll(t => t.UserId == request.UserId
                        && t.Id == request.TweetId
                        && t.IsDeleted == false)
                .GetQuery(_mapper, request.QueryOptions);
            return tweet;
        }
        #endregion
    }
}
