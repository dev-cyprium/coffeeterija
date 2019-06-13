using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using coffeterija.api.Services;
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
        readonly ITokenService<int, UserLoginDTO> tokenService;

        public AuthorizationController(ITokenService<int, UserLoginDTO> tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost]
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
    }
}
