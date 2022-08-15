using Microsoft.AspNetCore.Mvc;
using RegymBot.Data.Entities;
using RegymBot.Data.Repositories;
using System.Threading.Tasks;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly StaticMessageRepository _staticMessageRepository;

        public MessagesController(StaticMessageRepository staticMessageRepository)
        {
            _staticMessageRepository = staticMessageRepository;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _staticMessageRepository.GetAllAsync();

            return Ok(messages);
        }

        [HttpPost]
        [Route("update-message")]
        public async Task<IActionResult> UpdateMessage(StaticMessageEntity message)
        {
            await _staticMessageRepository.UpdateMessageAsync(message);

            return Ok();
        }
    }
}
