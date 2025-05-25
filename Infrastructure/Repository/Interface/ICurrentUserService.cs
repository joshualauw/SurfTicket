using SurfTicket.Infrastructure.Dto;

namespace SurfTicket.Infrastructure.Repository.Interface
{
    public interface ICurrentUserService
    {
        UserJwtPayload? Payload { get; }
    }
}
