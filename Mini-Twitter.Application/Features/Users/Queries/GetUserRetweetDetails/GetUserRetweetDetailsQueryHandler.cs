namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetDetails
{
    public class GetUserRetweetDetailsQueryHandler : IRequestHandler<GetUserRetweetDetailsQuery, IQueryable<Retweet>>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        #endregion

        #region Constructors
        public GetUserRetweetDetailsQueryHandler(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<Retweet>> Handle(GetUserRetweetDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserRetweetDetailsQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var retweet = _repo
                .GetRetweetByIdForUser(request.UserId, request.RetweetId);
            return retweet;
        }
        #endregion
    }
}
