using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegymBot.Services;
using System.Threading.Tasks;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public SchedulesController(IImageService imageService) 
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Route("upload-image")]
        public async Task<IActionResult> UploadImage(int club)
        {
            var file = Request.Form.Files[0];
            await _imageService.UploadImageAsync(file, club);

            return Ok();
        }
    }
}
