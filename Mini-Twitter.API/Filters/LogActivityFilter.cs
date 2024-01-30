namespace Mini_Twitter.API.Filters
{
    public class LogActivityFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Log.Information($"Starting execution on Controller: {context.Controller} action: {context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Log.Information($"Ending execution on Controller: {context.Controller} action: {context.ActionDescriptor.DisplayName}");
        }
    }
}
