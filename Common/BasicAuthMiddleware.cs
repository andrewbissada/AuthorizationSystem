using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string authHeader = context.Request.Headers["Authorization"]; // Retrieve the Authorization header from the incoming request
        if (authHeader != null)
        {
            var token = authHeader.Split(" ")[1]; // Split the Authorization header to get the token part (assuming "Bearer token_here" format)
            var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token)); // Decode the base64 encoded token to get the credential string
            var credentials = credentialString.Split(":"); // Split the credential string to separate username and password (assuming "username:password" format)
            if (credentials[0] == "username" && credentials[1] == "password")
            {
                await _next(context);
                return;
            }
        }
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }
}
