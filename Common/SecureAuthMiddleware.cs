using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

// public class SecureAuthMiddleware
// {
//     private readonly RequestDelegate _next;

//     public SecureAuthMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task InvokeAsync(HttpContext context)
//     {
//         // Implement JWT or other secure token validation here
//         // If valid, call the next middleware
//         // If invalid, set the HTTP status code to 401

//         await _next(context);
//     }
// }
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

public class SecureAuthMiddleware
{
    private readonly RequestDelegate _next;

    public SecureAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip middleware for login route
        if (context.Request.Path.StartsWithSegments("/api/auth/login"))
        {
            await _next(context);
            return;
        }

        string authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader != null && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler(); //// Create a new JwtSecurityTokenHandler to handle the incoming JWT token

                var validationParameters = GetValidationParameters(); // Retrieve the token validation parameters using the method GetValidationParameters()


                SecurityToken validatedToken; // Initialize a variable to hold the validated token

                // Validate the incoming JWT token using the ValidateToken method.
                // This will throw an exception if the validation fails.
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                // If the token is valid, continue to the next middleware in the pipeline
                await _next(context);
            }
            catch (SecurityTokenException se)
            {
                Console.WriteLine(se.Message); //for debugging purposes. Should be removed in production
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }
        }
        else
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            // Validate the issuer of the token
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a_very_long_and_random_secret_key_here_that_is_at_least_32_chars"))
        };
    }
}

