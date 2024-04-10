namespace RSMEnterpriseIntegrationsAPI.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.OpenApi.Extensions;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using System;
    using System.Diagnostics;
    using ValidationException = FluentValidation.ValidationException;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        private enum LoggingEvents
        {
            UNSPECIFIED = 0,
            VALIDATION_FAILURE = 1,
            BAD_REQUEST = 2,
            NOT_FOUND_RECORD = 3,
            UNAUTHORIZED_REQUEST = 4
        }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, context);
            }
        }

        private async Task HandleExceptionAsync(Exception ex, HttpContext context)
        {
            string? traceId = Activity.Current?.Id ?? context.TraceIdentifier;

            LogException(ex, _logger, traceId);

            var (statusCode, title) = MapException(ex);

            if (ex is ValidationException)
            {
                var validationException = ex as ValidationException;
                var errorsDictionary = validationException!.Errors
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(f => f.Key, f => f.ToArray());

                await Results.ValidationProblem(
                    title: title,
                    statusCode: statusCode,
                    errors: errorsDictionary,
                    extensions: new Dictionary<string, object?>
                    {
                        {nameof(traceId), traceId},
                    }
                ).ExecuteAsync(context);

            }
            else
            {
                await Results.Problem(
                    title: title,
                    statusCode: statusCode,
                    extensions: new Dictionary<string, object?>
                    {
                        {nameof(traceId), traceId},
                    }
                ).ExecuteAsync(context);
            }
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch
            {
                AuthenticationException => (StatusCodes.Status401Unauthorized, exception.Message),
                NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                ValidationException => (StatusCodes.Status400BadRequest, "One or more validation errors occurred."),
                BadRequestException => (StatusCodes.Status400BadRequest, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error.")
            };
        }

        private static void LogException(Exception exception, ILogger<ExceptionMiddleware> logger, string traceId)
        {

            (LogLevel logLevel, LoggingEvents logEvent) = exception switch
            {
                AuthenticationException => (LogLevel.Warning, LoggingEvents.UNAUTHORIZED_REQUEST),
                NotFoundException => (LogLevel.Warning, LoggingEvents.NOT_FOUND_RECORD),
                ValidationException => (LogLevel.Information, LoggingEvents.VALIDATION_FAILURE),
                BadRequestException => (LogLevel.Warning, LoggingEvents.BAD_REQUEST),
                _ => (LogLevel.Error, LoggingEvents.UNSPECIFIED)
            };

            var loggerMessage = LoggerMessage.Define<string, string>(
                 logLevel,
                 eventId: new EventId(id: (int)logEvent, name: logEvent.GetDisplayName()),
                 formatString: "  TraceID: {TraceID}\n\t{Message}\n"
             );

            loggerMessage(logger, traceId, exception.Message, exception);
        }
    }
}
