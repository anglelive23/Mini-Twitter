namespace Mini_Twitter.Application.Features.Tweets.Commands.UpdateTweetReply
{
    public class UpdateTweetReplyCommandHandler : IRequestHandler<UpdateTweetReplyCommand, Reply?>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public UpdateTweetReplyCommandHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<Reply?> Handle(UpdateTweetReplyCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTweetReplyCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkUpdate = await _repo
                .UpdateReplyForTweetAsync(request.TweetId, request.ReplyId, request.UpdateTweetReplyDto.Adapt<Reply>());
            return checkUpdate;
        }
        #endregion
    }
}
