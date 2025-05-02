using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SurfTicket.Presentation.Controllers.Admin
{
    [Route("api/admin/merchant")]
    [ApiController]
    [Authorize]
    public class MerchantController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISender _sender;
        public MerchantController(IConfiguration configuration, ISender sender)
        {
            _configuration = configuration;
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMerchant()
        {
            return Ok();
        }

    }
}
