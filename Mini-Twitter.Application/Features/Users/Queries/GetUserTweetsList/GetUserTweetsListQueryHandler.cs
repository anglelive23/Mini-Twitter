namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetsList
{
    public class GetUserTweetsListQueryHandler : IRequestHandler<GetUserTweetsListQuery, IQueryable<Tweet>>
    {
        private readonly IUserRepository _repo;

        public GetUserTweetsListQueryHandler(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IQueryable<Tweet>> Handle(GetUserTweetsListQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserTweetsListQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var tweets = _repo
                .GetTweetsForUser(request.UserId);
            return tweets;
        }
    }
}
