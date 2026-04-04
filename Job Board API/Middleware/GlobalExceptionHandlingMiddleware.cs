using System.Text.Json;
using Domain.Exceptions;

namespace Job_Board_API.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    } 
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException apiException)
        {
            context.Request.ContentType = "application/json";
            var problamDetail = new ApiProblemDitaile
            {
                Type = apiException.Type,
                Title = apiException.Title,
                StatusCode =  apiException.StatusCode,
                Details = apiException.Details,
                Instance = apiException.Instance
   
            };
            context.Response.StatusCode = problamDetail.StatusCode;
            var serialaz = JsonSerializer.Serialize(problamDetail ,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            
            await context.Response.WriteAsync(serialaz);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            var problemDatail = new ApiProblemDitaile
            {
                Type = "Internal Server Error",
                Title = "Title service error",
                StatusCode = 500,
                Details = "Internal server error",
                Instance = "Internal server  error"
            };
            context.Response.StatusCode = problemDatail.StatusCode;
    
            var serialaz = JsonSerializer.Serialize(problemDatail);
            await context.Response.WriteAsync(serialaz);
        } 
    }
}