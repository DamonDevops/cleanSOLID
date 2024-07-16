using HR_LeaveManagement.API.Models;
using HR_LeaveManagement.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HR_LeaveManagement.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);

        }catch(Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomValidationProblemDetails problem = new();

        switch (e)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomValidationProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomValidationProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(NotFoundException)
                };
                break;
            default:
                problem = new CustomValidationProblemDetails
                {
                    Title = e.Message,
                    Status = (int)statusCode,
                    Detail = e.StackTrace,
                    Type = nameof(e)
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        var logMessage = JsonConvert.SerializeObject(problem);
        _logger.LogError(e, logMessage);
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
