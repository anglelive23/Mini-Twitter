using Mini_Twitter.Application.Features.Tweets.Commands.DeleteTweet;

namespace Mini_Twitter.Application.Features.Tweets.Commands.DeleteTweetReply
{
    public class DeleteTweetReplyCommandHandler : IRequestHandler<DeleteTweetReplyCommand, bool>
    {
        #region Fields and Properties
        private readonly ITweetRepository _repo;
        #endregion

        #region Constructors
        public DeleteTweetReplyCommandHandler(ITweetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<bool> Handle(DeleteTweetReplyCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTweetReplyCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var checkDelete = await _repo
                .RemoveReplyForTweetAsync(request.TweetId, request.ReplyId);
            return checkDelete;
        }
        #endregion
    }
}
