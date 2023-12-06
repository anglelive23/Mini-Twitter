namespace Mini_Twitter.Application.Features.Tweets.Commands.CreateTweetReply
{
    public class CreateTweetReplyCommandHandler : IRequestHandler<CreateTweetReplyCommand, Reply?>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public CreateTweetReplyCommandHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<Reply?> Handle(CreateTweetReplyCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTweetReplyCommandValidator();
            await validator.ValidateAndThrowAsync(request);

            var reply = await _repo
                .AddReplyForTweetAsync(request.Id, request.ReplyDto.Adapt<Reply>());

            return reply;
        }
        #endregion
    }
}
