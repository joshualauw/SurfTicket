using SurfTicket.Infrastructure.Dto;

namespace SurfTicket.Application.Services.Interface
{
    public interface ICurrentUserService
    {
        public UserJwtPayload Payload { get; }
    }
}
