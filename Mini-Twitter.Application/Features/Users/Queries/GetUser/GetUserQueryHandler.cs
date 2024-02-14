namespace Mini_Twitter.Application.Features.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ApplicationUserDto?>
    {
        #region Fields and Properties
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        #endregion

        #region Constructors
        public GetUserQueryHandler(IUserRepository repo, IMapper mapper, ICacheService cacheService)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }
        #endregion

        #region Interface Implementation
        public async Task<ApplicationUserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var user = await _cacheService.GetAsync(null, async () =>
            {
                var user = await _repo
                .GetAll(u => u.Id == request.UserId && u.IsDeleted == false)
                .GetQueryAsync(_mapper, request.QueryOptions);

                return user.FirstOrDefault()!;
            }, CacheOptions.DefaultExpiration
            , cancellationToken);

            return user;
        }
        #endregion
    }
}
