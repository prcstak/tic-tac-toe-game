using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace account.Middleware;

public class CustomExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlingMiddleware(RequestDelegate next)
        => _next = next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = string.Empty;
        var errorCode = exception switch
        {
            WrongCredentialsException => HttpStatusCode.BadRequest,
            UserAlreadyExistsException => HttpStatusCode.Unauthorized,
            PasswordTooShortException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) errorCode;
        
        if (error == string.Empty)
            error = JsonSerializer.Serialize(new { error = exception.Message });

        await context.Response.WriteAsync(error);
    }
}

public static class CustomExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this
        IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlingMiddleware>();
    }
}
