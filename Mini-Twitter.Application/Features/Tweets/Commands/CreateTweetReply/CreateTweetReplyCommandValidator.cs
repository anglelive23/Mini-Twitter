namespace Mini_Twitter.Application.Features.Tweets.Commands.CreateTweetReply
{
    public class CreateTweetReplyCommandValidator : AbstractValidator<CreateTweetReplyCommand>
    {
        public CreateTweetReplyCommandValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty().WithMessage("{PropertyName} is required!");

            RuleFor(r => r.ReplyDto.Context)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MaximumLength(256).WithMessage("{PropertyName} max length is 256!");

            RuleFor(r => r.ReplyDto.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
