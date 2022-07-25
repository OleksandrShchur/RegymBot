using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.Massage
{
    public class HandleMassage : BaseHandle<HandleMassage>
    {
        public HandleMassage(ILogger<HandleMassage> logger,
            ITelegramBotClient botClient)
                : base(logger, botClient)
        { }

        public async Task BotOnMassage(Message message)
        {
            _logger.LogInformation("Receive message type in massage: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;
        }
    }
}
