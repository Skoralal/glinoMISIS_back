using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace glinoMISIS_back.Extentions
{
    public static class ApiExtentions
    {
        static JwtOptions jwtOptions = new JwtOptions();

        static readonly private TokenValidationParameters _validationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            
        };
        public static void AddApiAuth(
            this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.TokenValidationParameters = _validationParameters;

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["notjwttoken"];
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();
        }
        public static string DecipherJWT(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, _validationParameters, out SecurityToken validatedToken);

                // Extract information (claims) from JWT body
                var claims = principal.Claims;
                var login = claims.FirstOrDefault(c => c.Type == "employeeLogin")?.Value;

                return login;
            }
            catch (SecurityTokenExpiredException)
            {
                return "Token has expired";
            }
            catch (Exception)
            {
                return "Token is invalid";
            }
        }
    }
}
