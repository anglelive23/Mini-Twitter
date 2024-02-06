namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetsList
{
    public class GetUserRetweetsListQueryHandler : IRequestHandler<GetUserRetweetsListQuery, IQueryable<RetweetDto>>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GetUserRetweetsListQueryHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<RetweetDto>> Handle(GetUserRetweetsListQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserRetweetsListQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var retweets = _repo
                .GetRetweetsForUser(request.UserId)
                .GetQuery(_mapper, request.QueryOptions);
            return retweets;
        }
        #endregion
    }
}
