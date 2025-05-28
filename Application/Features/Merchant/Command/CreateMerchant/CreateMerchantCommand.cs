using MediatR;

namespace SurfTicket.Application.Features.Merchant.Command.CreateMerchant
{
    public class CreateMerchantCommand : IRequest<CreateMerchantCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
