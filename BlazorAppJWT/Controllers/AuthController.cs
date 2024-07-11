using BlazorAppJWT.Client.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorAppJWT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            if (userLogin.Username == "User" && userLogin.Password == "Pass")
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userLogin.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b2a1badfd4f2ecaf96aec5b4a683d67baab144cb4b5265b93f957d5f264a61e2"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "issuer",
                    audience: "audience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Forbid();
        }
    }
}
