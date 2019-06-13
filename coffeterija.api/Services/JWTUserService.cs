using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using coffeterija.application.Requests;
using coffeterija.dataaccess;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace coffeterija.api.Services
{
    public class JWTUserService : ITokenService<int, UserLoginDTO>
    {
        public class InvalidTokenException : Exception
        {
            public InvalidTokenException()
                : base("The sent token is not valid")
            { }
        }

        CoffeeContext context;
        IConfiguration config;

        public JWTUserService(CoffeeContext context, IConfiguration config)
        {
            this.config = config;
            this.context = context;
        }

        /// <summary>
        /// Generates the JWT token based on the request.
        /// </summary>
        /// <returns>Returns the JWT token</returns>
        public string GenerateToken(UserLoginDTO request)
        {
            var user = context.Users.SingleOrDefault(u => u.Email == request.Email);

            if (user == null)
                return null;

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
            return tokenHanlder.WriteToken(token);
        }

        public int GetFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                handler.ValidateToken(token, Policy(), out SecurityToken validToken);

                var jwt = handler.ReadToken(token) as JwtSecurityToken;
                var id = jwt.Claims.First(claim => claim.Type == "user_id").Value;

                return int.Parse(id);

            } catch(Exception)
            {
                throw new InvalidTokenException();
            }
        }

        private TokenValidationParameters Policy()
        {
            var key = Encoding.ASCII.GetBytes(config.GetSection("Encryption")["key"]);
            return new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    }
}
