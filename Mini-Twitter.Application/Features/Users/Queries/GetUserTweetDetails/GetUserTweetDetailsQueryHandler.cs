namespace Mini_Twitter.Application.Features.Users.Queries.GetUserTweetDetails
{
    public class GetUserTweetDetailsQueryHandler : IRequestHandler<GetUserTweetDetailsQuery, IQueryable<Tweet>>
    {
        private readonly ITweetRepository _repo;

        public GetUserTweetDetailsQueryHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IQueryable<Tweet>> Handle(GetUserTweetDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserTweetDetailsQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var tweet = _repo
                .GetAll(t => t.UserId == request.UserId
                        && t.Id == request.TweetId
                        && t.IsDeleted == false);
            return tweet;
        }
    }
}
