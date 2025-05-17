using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SurfTicket.Presentation.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISender _sender;

        public SubscriptionController(IConfiguration configuration, ISender sender)
        {
            _configuration = configuration;
            _sender = sender;
        }
    }
}
