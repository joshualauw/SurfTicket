using SurfTicket.Application.Services.Interface;
using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Dto;

namespace SurfTicket.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public UserJwtPayload Payload { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Payload = new UserJwtPayload();
            var context = httpContextAccessor.HttpContext;

            if (context?.User.Identity?.IsAuthenticated == true)
            {
                var claims = context.User.Claims.ToDictionary(c => c.Type, c => c.Value);

                Payload.UserId = claims.GetValueOrDefault("UserId", "");
                Payload.Email = claims.GetValueOrDefault("Email", "");
                Payload.Username = claims.GetValueOrDefault("Username", "");
                Payload.FirstName = claims.GetValueOrDefault("FirstName", "");
                Payload.LastName = claims.GetValueOrDefault("LastName", "");
                Payload.ActivePlan = Enum.Parse<PlanCode>(claims.GetValueOrDefault("ActivePlan", "BASIC"));
            }
        }
    }
}
