namespace Mini_Twitter.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        #region Fields and Properties
        private readonly RequestDelegate _next;
        #endregion

        #region Constructors
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        #endregion


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleGlobalExceptionAsync(context, ex);
            }
        }

        private Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = context.TraceIdentifier;
            switch (exception)
            {
                case FluentValidation.ValidationException validationException:
                    {
                        StringBuilder message = new StringBuilder();
                        foreach (var error in validationException.Errors)
                        {
                            message.AppendLine(error.ErrorMessage);
                        }
                        Log.ForContext("ValidationError", message.ToString())
                            .Error($"Validation error occurred in Application Layer in request: {0}.", traceId);
                        return WriteErrorResponse(context, HttpStatusCode.BadRequest, traceId, $"Validation Error occurred in Application Layer in request: {traceId}.", message.ToString());
                    }

                case DataFailureException dataFailureException:
                    {
                        var message = dataFailureException.InnerException != null ? dataFailureException.InnerException.Message : dataFailureException.Message;
                        Log.ForContext("DataFailureError", message)
                            .Error($"Data Failure Error occurred in Infrastructure Layer, Contact system admin with request id: {0}.", traceId);
                        return WriteErrorResponse(context, HttpStatusCode.InternalServerError, traceId, $"Data Failure Error occurred in Infrastructure Layer in request: {traceId}.", message);
                    }

                default:
                    {
                        Log.ForContext("TraceIdentifier", traceId)
                           .Error(exception, "An Error occurd in request: {0}.", traceId);
                        return WriteErrorResponse(context, HttpStatusCode.InternalServerError, traceId, $"An exception was thrown in the system in request: {traceId}.", exception.Message);
                    }
            }
        }

        private Task WriteErrorResponse(HttpContext context, HttpStatusCode code, string traceId, string message, string? details = null)
        {
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsJsonAsync(new
            {
                TraceIdentifier = traceId,
                Message = message,
                ErrorMessage = details ?? message
            });
        }
    }
}
