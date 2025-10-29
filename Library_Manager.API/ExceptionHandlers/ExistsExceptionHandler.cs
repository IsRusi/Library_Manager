using Library_Manager.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library_Manager.API.ExceptionHandlers
{
    internal class ExistsExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger<ExistsExceptionHandler> _logger;

        public ExistsExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<ExistsExceptionHandler> logger)
        {
            _problemDetailsService = problemDetailsService;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not (BookAlreadyExistsException or AuthorAlreadyExistsException))
                return false;

            _logger.LogWarning(exception, $"Обнаружен дубликат: {exception.Message}");

            httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

            var details = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Обнаружен дубликат",
                Detail = exception.Message,
                Status = StatusCodes.Status409Conflict
            };

            var context = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = details
            };

            await _problemDetailsService.TryWriteAsync(context);

            return true;
        }
    }
}
