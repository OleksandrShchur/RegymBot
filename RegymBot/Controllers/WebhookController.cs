using Microsoft.AspNetCore.Mvc;
using RegymBot.Handlers;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace RegymBot.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdate handleUpdate,
                                              [FromBody] Update update)
        {
            await handleUpdate.EchoAsync(update);

            return Ok();
        }
    }
}
