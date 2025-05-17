using SurfTicket.Domain.Enums;

namespace SurfTicket.Infrastructure.Dto
{
    public class UserJwtPayload
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PlanCode ActivePlan {  get; set; }
    }
}
