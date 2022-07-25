using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.Price
{
    public class HandlePrice : BaseHandle<HandlePrice>
    {
        public HandlePrice(ILogger<HandlePrice> logger,
            ITelegramBotClient botClient)
                : base(logger, botClient)
        { }

        public async Task BotOnPrice(Message message)
        {
            _logger.LogInformation("Receive message type in price: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;
        }
    }
}
