using VEJuicios.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly JWTSettings _jwtSettings;

        //public JwtMiddleware(RequestDelegate next, IOptions<JWTSettings> jwtSettings)
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
            //_jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                var key = Encoding.ASCII.GetBytes("SIAJaaaaaaaaaaa");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                User user = new User();
                user.ID = jwtToken.Claims.First(x => x.Type == "ID").Value;
                user.Apellido = jwtToken.Claims.First(x => x.Type == "Apellido").Value;
                user.Nombre = jwtToken.Claims.First(x => x.Type == "Nombre").Value;
                user.UserName = jwtToken.Claims.First(x => x.Type == "UserName").Value;
                //user.Perfil = jwtToken.Claims.First(x => x.Type == "Perfil").Value;

                // attach user to context on successful jwt validation
                context.Items["User"] = user;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}