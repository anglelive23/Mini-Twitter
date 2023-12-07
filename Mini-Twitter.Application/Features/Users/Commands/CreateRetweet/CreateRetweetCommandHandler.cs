namespace Mini_Twitter.Application.Features.Users.Commands.CreateRetweet
{
    public class CreateRetweetCommandHandler : IRequestHandler<CreateRetweetCommand, Retweet?>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        #endregion

        #region Constructors
        public CreateRetweetCommandHandler(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<Retweet?> Handle(CreateRetweetCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRetweetCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkAdd = await _repo
                .AddRetweetForUserAsync(request.UserId, request.RetweetDto.TweetId);
            return checkAdd;
        }
        #endregion
    }
}
