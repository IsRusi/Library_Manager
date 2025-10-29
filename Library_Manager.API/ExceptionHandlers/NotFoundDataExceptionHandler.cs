using Library_Manager.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library_Manager.API.ExceptionHandlers
{
    public class NotFoundDataExceptionHandler:IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger<NotFoundDataExceptionHandler> _logger;

        public NotFoundDataExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<NotFoundDataExceptionHandler> logger)
        {
            _problemDetailsService = problemDetailsService;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
                 HttpContext httpContext,
                 Exception exception,
                 CancellationToken cancellationToken)
            {
                if (exception is not (BookNotFoundException or AuthorNotFoundException ))
                {
                    return false;
                }

                _logger.LogWarning(exception, $"Запрошенный ресурс не найден: {exception.Message}");

            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

            await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
            {
                Type = exception.GetType().Name,
                Title = "Ресурс не найден",
                Detail = exception.Message,
                Status = StatusCodes.Status404NotFound,
            }
            });


            return true;
            }
        }
    }