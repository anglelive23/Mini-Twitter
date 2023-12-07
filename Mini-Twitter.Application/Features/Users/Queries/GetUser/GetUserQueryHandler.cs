namespace Mini_Twitter.Application.Features.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IQueryable<ApplicationUser>>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        #endregion

        #region Constructors
        public GetUserQueryHandler(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<ApplicationUser>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var user = _repo
                .GetAll(u => u.Id == request.UserId && u.IsDeleted == false);
            return user;
        }
        #endregion
    }
}
