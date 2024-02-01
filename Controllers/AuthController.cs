using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult Login(LoginModel model)
    {
        // Validate the user's credentials (e.g., against a database)
        if (IsValidUser(model))
        {
            // Generate a JWT token
            string jwtToken = GenerateJwtToken(model.Username);

            // Return the token
            return Ok(new { Token = jwtToken });
        }
        
        return Ok("Debug message");//return Unauthorized();
    }

    private bool IsValidUser(LoginModel model)
    {
        // Validate the user here (e.g., check the database)       
       // return true;
        return model.Username == "username" && model.Password == "password";
        
    }

    private string GenerateJwtToken(string username)
    {
        // // Generate and return a JWT token using a library like System.IdentityModel.Tokens.Jwt
        // Define the token's claims
        // Create a set of claims that identify the user. Claims are key-value pairs embedded in the token.
        // JwtRegisteredClaimNames.Sub represents the subject of the token (usually the user) and JwtRegisteredClaimNames.Jti is a unique identifier for the token.
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Generate the token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a_very_long_and_random_secret_key_here_that_is_at_least_32_chars"));
        //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkey")); // Make sure to store this securely
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "yourdomain.com", //// Issuer should represent your domain
            audience: "yourdomain.com", // Audience typically represents the recipients that the token is intended for
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), // Token will expire in 30 minutes
            signingCredentials: creds  
        );
        // Serialize the token object into a JWT string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
