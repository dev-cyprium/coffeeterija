using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using coffeterija.api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        public async Task Invoke(HttpContext httpContext, ILoginService service, IConfiguration config)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString();
            if(!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var key = Encoding.ASCII.GetBytes(config.GetSection("Encryption")["key"]);
                    handler.ValidateToken(token, new TokenValidationParameters
                    {
                         RequireExpirationTime = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(key),
                         ValidateIssuer = false,
                         ValidateAudience = false
                    }, out SecurityToken validatedToken);
                    var jwt = handler.ReadToken(token) as JwtSecurityToken;
                    var id = jwt.Claims.First(claim => claim.Type == "user_id").Value;

                    service.LoginWithId(int.Parse(id));
                    await _next(httpContext);
                } catch (Exception)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Bad token");
                }
            }
        }
    }
}
