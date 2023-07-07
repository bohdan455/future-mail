using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpGet("{code:guid}")]
        public async Task<IActionResult> VerifyAccount(Guid code)
        {
            if(await _emailService.VerifyAsync(code))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
