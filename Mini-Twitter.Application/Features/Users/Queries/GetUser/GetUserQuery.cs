namespace Mini_Twitter.Application.Features.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<IQueryable<ApplicationUserDto>>
    {
        public string UserId { get; set; }
        public required ODataQueryOptions<ApplicationUserDto> QueryOptions { get; set; }
    }
}
