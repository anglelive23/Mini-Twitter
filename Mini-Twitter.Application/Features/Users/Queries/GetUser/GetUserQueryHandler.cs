namespace Mini_Twitter.Application.Features.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IQueryable<ApplicationUserDto>>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GetUserQueryHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion

        #region Interface Implementation
        public async Task<IQueryable<ApplicationUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            var user = _repo
                .GetAll(u => u.Id == request.UserId && u.IsDeleted == false)
                .GetQuery(_mapper, request.QueryOptions);
            return user;
        }
        #endregion
    }
}
