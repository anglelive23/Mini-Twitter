namespace Mini_Twitter.Application.Features.Users.Queries.GetUserRetweetDetails
{
    public class GetUserRetweetDetailsQuery : IRequest<IQueryable<RetweetDto>>
    {
        public required string UserId { get; set; }
        public required int RetweetId { get; set; }
        public required ODataQueryOptions<RetweetDto> QueryOptions { get; set; }
    }
}
