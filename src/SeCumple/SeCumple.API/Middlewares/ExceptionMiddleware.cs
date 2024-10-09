using System.Net;
using Newtonsoft.Json;
using SeCumple.Application.Dtos.Response;
using SeCumple.CrossCutting.Errors;
using SeCumple.CrossCutting.Exceptions;

namespace SeCumple.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (e)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ValidationException validationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    var errors = validationException.Errors.SelectMany(x => x.Value).ToArray();
                    var validationJson = JsonConvert.SerializeObject(errors);
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, errors, validationJson));
                    break;
                case BadRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new ProcessResult<CodeErrorException>
                {
                    IsSuccess = false,
                    Messages = new[] { e.Message }
                });
            }
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}