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
            var errorCode = Guid.NewGuid();
            switch (exception)
            {
                case ApplicationException applicationException:
                    {
                        Log.ForContext("ApplicationError", applicationException.Message)
                            .Error($"An error with code: {errorCode} occurred in Application Layer.");
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return context.Response.WriteAsJsonAsync(new
                        {
                            ErrorCode = errorCode,
                            Message = "An error occurred in Application Layer.",
                            ErrorMessage = applicationException.InnerException!.Message ?? applicationException.Message
                        });
                    }

                case FluentValidation.ValidationException validationException:
                    {
                        StringBuilder message = new StringBuilder();
                        foreach (var error in validationException.Errors)
                        {
                            message.AppendLine(error.ErrorMessage);
                        }
                        Log.ForContext("ValidationError", message.ToString())
                            .Error($"Validation error with code: {errorCode} occurred in Application Layer.");
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return context.Response.WriteAsJsonAsync(new
                        {
                            ErrorCode = errorCode,
                            Message = "Validation Error occurred in Application Layer.",
                            ErrorMessage = message.ToString()
                        });
                    }

                case DataFailureException dataFailureException:
                    {
                        var message = dataFailureException.InnerException != null ? dataFailureException.InnerException.Message : dataFailureException.Message;
                        Log.ForContext("DataFailureError", message)
                            .Error($"Data Failure Error with code: {errorCode} occurred in Infrastructure Layer, Contact system admin with ErrorCode.");
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return context.Response.WriteAsJsonAsync(new
                        {
                            ErrorCode = errorCode,
                            Message = "Data Failure Error occurred in Infrastructure Layer.",
                            ErrorMessage = message
                        });
                    }

                case NotFoundException NotFoundException:
                    {
                        var message = NotFoundException.InnerException != null ? NotFoundException.InnerException.Message : NotFoundException.Message;
                        Log.ForContext("NotFoundException", message)
                            .Error($"NotFoundException with code: {errorCode} occurred in Application Layer, Contact system admin with ErrorCode.");
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return context.Response.WriteAsJsonAsync(new
                        {
                            ErrorCode = errorCode,
                            Message = $"NotFoundException with code: {errorCode} occurred in Application Layer, Contact system admin with ErrorCode.",
                            ErrorMessage = message
                        });
                    }

                case Application.Exceptions.ValidationException validationException:
                    {
                        var message = validationException.InnerException != null ? validationException.InnerException.Message : validationException.Message;
                        Log.ForContext("ValidationException", message)
                            .Error($"ValidationException with code: {errorCode} occurred in Application Layer, Check out your credintals.");
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return context.Response.WriteAsJsonAsync(new
                        {
                            ErrorCode = errorCode,
                            Message = $"ValidationException with code: {errorCode} occurred in Application Layer, Check out your credintals.",
                            ErrorMessage = message
                        });
                    }

                case IOException iOException:
                    {
                        var message = iOException.InnerException != null ? iOException.InnerException.Message : iOException.Message;
                        Log.ForContext("IOException", message)
                            .Error($"IOException Error with code: {errorCode} occurred in Infrastructure Layer, Image saving failed.");
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return context.Response.WriteAsJsonAsync(new
                        {
                            ErrorCode = errorCode,
                            Message = "IOException Error occurred in Infrastructure Layer, Image saving failed.",
                            ErrorMessage = message
                        });
                    }

                default:
                    {
                        Log.ForContext("ErrorCode", errorCode)
                           .Error(exception, $"An Error with code: {errorCode} occured in API");
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return context.Response.WriteAsJsonAsync(new
                        {
                            ErrorCode = errorCode,
                            Message = "An exception was thrown in the system.",
                            ErrorMessage = exception.Message
                        });
                    }
            }
        }
    }
}
