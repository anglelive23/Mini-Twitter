namespace Mini_Twitter.Application.Features.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<IQueryable<ApplicationUser>>
    {
        public string UserId { get; set; }
    }
}
