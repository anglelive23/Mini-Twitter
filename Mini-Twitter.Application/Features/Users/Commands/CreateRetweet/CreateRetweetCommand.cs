namespace Mini_Twitter.Application.Features.Users.Commands.CreateRetweet
{
    public class CreateRetweetCommand : IRequest<RetweetDto?>
    {
        public string UserId { get; set; }
        public CreateRetweetDto RetweetDto { get; set; }
    }
}
