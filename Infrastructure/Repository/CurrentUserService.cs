using SurfTicket.Domain.Enums;
using SurfTicket.Infrastructure.Dto;
using SurfTicket.Infrastructure.Repository.Interface;

namespace SurfTicket.Infrastructure.Repository
{
    public class CurrentUserService : ICurrentUserService
    {
        public UserJwtPayload? Payload { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext;
            if (context?.User.Identity?.IsAuthenticated == true)
            {
                var claims = context.User.Claims.ToDictionary(c => c.Type, c => c.Value);

                Payload = new UserJwtPayload
                {
                    UserId = claims.GetValueOrDefault("UserId", ""),
                    Email = claims.GetValueOrDefault("Email", ""),
                    Username = claims.GetValueOrDefault("Username", ""),
                    FirstName = claims.GetValueOrDefault("FirstName", ""),
                    LastName = claims.GetValueOrDefault("LastName", ""),
                    ActivePlan = Enum.Parse<PlanCode>(claims.GetValueOrDefault("ActivePlan", "BASIC"))
                };
            }
        }
    }
}
