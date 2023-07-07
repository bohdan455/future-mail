using BLL.Dto;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LetterController : ControllerBase
    {
        private readonly IFutureMailService _futureMailService;

        public LetterController(IFutureMailService futureMailService)
        {
            _futureMailService = futureMailService;
        }
        [HttpPost]
        public async Task<IActionResult> PostLetter(EmailLetterDto emailLetterDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            if (!await _futureMailService.SendAsync(emailLetterDto))
            {
                return BadRequest("Email isn't verified");
            }
            return Ok();
        }
    }
}
