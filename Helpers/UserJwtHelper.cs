using Microsoft.IdentityModel.Tokens;
using SurfTicket.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurfTicket.Helpers
{
    public class UserJwtHelper
    {
        public static UserJwtPayload GetJwtUser(HttpContext context)
        {
            Dictionary<string, string> Claims = new Dictionary<string, string>();
            UserJwtPayload payload = new UserJwtPayload();

            if (context.User.Identity?.IsAuthenticated == true)
            {
                var tokenString = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(tokenString);

                Claims = jsonToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                payload = new UserJwtPayload
                {
                    ApplicationUserID = Claims["userID"],
                };
            }

            return payload;
        }

        public static string GenerateJwtToken(IConfiguration Configuration, string userId)
        {
            var jwtSettings = Configuration.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userID", userId)
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
