using Library_Manager.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal class ExceptionsHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<ExceptionsHandler> _logger;

    public ExceptionsHandler(IProblemDetailsService problemDetailsService, ILogger<ExceptionsHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception,$"Произошла необработанная ошибка: {exception.Message}");

        httpContext.Response.StatusCode = exception switch
        {
                //Author 
                InvalidAuthorOperationException => StatusCodes.Status409Conflict,
                AuthorNotFoundException => StatusCodes.Status404NotFound,
                InvalidIdException => StatusCodes.Status400BadRequest,
                InvalidDateOfBirthException => StatusCodes.Status400BadRequest,
                InvalidAgeException => StatusCodes.Status400BadRequest,
                InvalidNameException => StatusCodes.Status400BadRequest,
                AuthorAlreadyExistsException => StatusCodes.Status409Conflict,

                //Book
                BookNotFoundException => StatusCodes.Status404NotFound,
                InvalidBookIdException => StatusCodes.Status400BadRequest,
                InvalidTitleException => StatusCodes.Status400BadRequest,
                BookAlreadyExistsException => StatusCodes.Status409Conflict,
                InvalidPublishedYearException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

        var details = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Title = "Произошла ошибка",
            Detail = exception.Message,
            Status = httpContext.Response.StatusCode
        };

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = details
        });
    }
}