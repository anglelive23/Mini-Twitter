namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetsList
{
    public class GetUserRetweetsListQuery : IRequest<IQueryable<RetweetDto>>
    {
        public string UserId { get; set; }
        public required ODataQueryOptions<RetweetDto> QueryOptions { get; set; }
    }
}
