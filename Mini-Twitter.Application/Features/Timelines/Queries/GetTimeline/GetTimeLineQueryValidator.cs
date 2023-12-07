namespace Mini_Twitter.Application.Features.Timelines.Queries.GetTimeline
{
    public class GetTimeLineQueryValidator : AbstractValidator<GetTimeLineQuery>
    {
        public GetTimeLineQueryValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(u => u.PageNumber).NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(u => u.PageSize).NotEmpty().WithMessage("{PropertyName} is required!");
        }
    }
}
