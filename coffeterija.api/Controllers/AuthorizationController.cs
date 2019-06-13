using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using coffeterija.dataaccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api.Controllers
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        CoffeeContext context;
        IConfiguration config;

        public AuthorizationController(CoffeeContext context, IConfiguration config)
        {
            this.config = config;
            this.context = context;
        }

        [HttpPost]
        public IActionResult Login(UserLoginDTO request)
        {
            var user = context.Users.SingleOrDefault(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized();

            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.GetSection("Encryption")["key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHanlder.CreateToken(tokenDescriptor);
            var response = tokenHanlder.WriteToken(token);

            return Ok(response);
        }
    }
}
