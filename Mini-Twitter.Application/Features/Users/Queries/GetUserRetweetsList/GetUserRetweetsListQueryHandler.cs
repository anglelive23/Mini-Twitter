namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetsList
{
    public class GetUserRetweetsListQueryHandler : IRequestHandler<GetUserRetweetsListQuery, IQueryable<Retweet>>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        #endregion

        #region Constructors
        public GetUserRetweetsListQueryHandler(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<Retweet>> Handle(GetUserRetweetsListQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserRetweetsListQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var retweets = _repo
                .GetRetweetsForUser(request.UserId);
            return retweets;
        }
        #endregion
    }
}
