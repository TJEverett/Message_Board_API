using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MessageBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MessageBoard.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoginController : Controller
  {
    private IConfiguration _config;

    public LoginController(IConfiguration config)
    {
      _config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login ([FromBody]UserModel login)
    {
      IActionResult response = Unauthorized();
      var user = AuthenticateUser(login);

      if (user != null)
      {
        var tokenString = GenerateJSONWebToken(user);
        response = Ok(new { token = tokenString });
      }

      return response;
    }

    private string GenerateJSONWebToken(UserModel userInfo)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[] {
        new Claim("Username", userInfo.Username)
      };

      var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        _config["Jwt:Issuer"],
        claims,
        expires:DateTime.Now.AddMinutes(180),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserModel AuthenticateUser(UserModel login)
    {
      UserModel user = null;

      // Demo Check for between 2 fake users
      if (login.Username.ToLower() == "crazycat")
      {
        user = new UserModel { Username = "CrazyCat" };
      }
      else if (login.Username.ToLower() == "underdog")
      {
        user = new UserModel { Username = "UnderDog" };
      }

      return user;
    }
  }
}