namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetDetails
{
    public class GetUserRetweetDetailsQueryHandler : IRequestHandler<GetUserRetweetDetailsQuery, IQueryable<RetweetDto>>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GetUserRetweetDetailsQueryHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<RetweetDto>> Handle(GetUserRetweetDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserRetweetDetailsQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var retweet = _repo
                .GetRetweetByIdForUser(request.UserId, request.RetweetId)
                .GetQuery(_mapper, request.QueryOptions);
            return retweet;
        }
        #endregion
    }
}
