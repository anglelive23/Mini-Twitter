namespace Mini_Twitter.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        #endregion

        #region Constcutors
        public DeleteUserCommandHandler(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteUserCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var checkDelete = await _repo
                .DeleteUserAsync(request.UserId);
            return checkDelete;
        }
        #endregion
    }
}
