using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using coffeterija.api.Services;
using coffeterija.application.Commands.Users;
using coffeterija.application.Requests;
using coffeterija.application.Responses;
using coffeterija.dataaccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly ITokenService<int, UserLoginDTO> tokenService;
        private readonly IPasswordService passwordService;
        private readonly IRegisterUser registerCommand;

        public AuthorizationController(
            ITokenService<int, UserLoginDTO> tokenService,
            IPasswordService passwordService,
            IRegisterUser registerCommand)
        {
            this.tokenService = tokenService;
            this.passwordService = passwordService;
            this.registerCommand = registerCommand;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO request)
        {

            var token = tokenService.GenerateToken(request);

            if (token == null)
                return Unauthorized();

            return Ok(new TokenResponse()
            {
                Token = token
            });
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDTO request)
        {
            request.Password = passwordService.HashPassword(request.Password);
            registerCommand.Execute(request);
            
            return Ok();
        }
    }
}
