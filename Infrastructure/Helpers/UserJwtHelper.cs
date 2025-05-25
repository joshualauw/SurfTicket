using Microsoft.IdentityModel.Tokens;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurfTicket.Infrastructure.Helpers
{
    public class UserJwtHelper
    {
        public static UserJwtPayload GetJwtUser(HttpContext context)
        {
            UserJwtPayload payload = new UserJwtPayload();

            if (context.User.Identity?.IsAuthenticated == true)
            {
                var tokenString = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(tokenString);

                var Claims = jsonToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                payload = new UserJwtPayload
                {
                    UserId = Claims["UserId"],
                    Email = Claims["Email"],
                    Username = Claims["Username"],
                    FirstName = Claims["FirstName"],    
                    LastName = Claims["LastName"],
                    ActivePlan = (PlanCode) Enum.Parse(typeof(PlanCode), Claims["ActivePlan"].ToString())
                };
            }

            return payload;
        }

        public static string GenerateJwtToken(IConfiguration Configuration, UserJwtPayload payload)
        {
            var jwtSettings = Configuration.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var props = payload.GetType().GetProperties();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, payload.UserId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", payload.UserId),
                new Claim("Email", payload.Email),
                new Claim("Username", payload.Username),
                new Claim("FirstName", payload.FirstName),
                new Claim("LastName", payload.LastName),
                new Claim("ActivePlan", payload.ActivePlan.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(jwtSettings["ExpiryInDays"] ?? "")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
