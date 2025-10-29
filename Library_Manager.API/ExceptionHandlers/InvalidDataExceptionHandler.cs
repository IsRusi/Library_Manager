using Library_Manager.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Library_Manager.API.ExceptionHandlers
{
    internal class InvalidDataExceptionHandler:IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger<InvalidDataExceptionHandler> _logger;

        public InvalidDataExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<InvalidDataExceptionHandler> logger)
        {
            _problemDetailsService = problemDetailsService;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
             HttpContext httpContext,
             Exception exception,
             CancellationToken cancellationToken)
        {
            if (exception is not
                                (InvalidAgeException
                                or InvalidBookIdException
                                or InvalidDateOfBirthException
                                or InvalidIdException
                                or InvalidNameException
                                or InvalidPublishedYearException
                                or InvalidTitleException
                                or InvalidAuthorOperationException)
                                )
            {
                return false;
            }

            _logger.LogWarning(exception, $"Введенные данные не корректны: {exception.Message}");

            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

            await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
            {
                Type = exception.GetType().Name,
                Title = "Данные введены не верно",
                Detail = exception.Message,
                Status = StatusCodes.Status404NotFound,
            }
            });

            return true;
        }
    }
}
