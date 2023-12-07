namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetsList
{
    public class GetUserTweetsListQueryHandler : IRequestHandler<GetUserTweetsListQuery, IQueryable<Tweet>>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        #endregion

        #region Constuctors
        public GetUserTweetsListQueryHandler(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<Tweet>> Handle(GetUserTweetsListQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserTweetsListQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var tweets = _repo
                .GetTweetsForUser(request.UserId);
            return tweets;
        }
        #endregion
    }
}
