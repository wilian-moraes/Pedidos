// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;

// [ApiController]
// [Route("api/[controller]")]
// public class AuthController : ControllerBase
// {
    // [HttpPost("login")]
    // public IActionResult Login([FromBody] LoginModel user)
    // {
        // if (user.Username == "admin" && user.Password == "1234")
        // {
            // var key = Encoding.ASCII.GetBytes("uma-chave-muito-secreta-e-grande");
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
                // Subject = new ClaimsIdentity(new Claim[]
                // {
                    // new Claim(ClaimTypes.Name, user.Username)
                // }),
                // Expires = DateTime.UtcNow.AddHours(1),
                // SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            // };
            // var token = tokenHandler.CreateToken(tokenDescriptor);
            // var tokenString = tokenHandler.WriteToken(token);

            // return Ok(new { Token = tokenString });
        // }

        // return Unauthorized();
    // }
// }

// public class LoginModel
// {
    // public string Username { get; set; }
    // public string Password { get; set; }
// }
