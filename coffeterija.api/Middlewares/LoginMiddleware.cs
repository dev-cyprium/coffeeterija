using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using coffeterija.api.Services;
using coffeterija.application.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static coffeterija.api.Services.JWTUserService;

namespace coffeterija.api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, 
            ILoginService loginService, 
            ITokenService<int, UserLoginDTO> tokenService)
        {
            httpContext.Response.Headers["Access-Control-Allow-Origin"] = "*";

            var token = httpContext.Request.Headers["Authorization"].ToString();
            if(!string.IsNullOrEmpty(token))
            {
                try
                {
                    int id = tokenService.GetFromToken(token);
                    loginService.LoginWithId(id);
                    await _next(httpContext);
                } catch (InvalidTokenException)
                {
                    await _next(httpContext);
                }
            } else
            {
                await _next(httpContext);
            }
        }
    }
}
