using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.Solarium
{
    public class HandleSolarium : BaseHandle<HandleSolarium>
    {
        public HandleSolarium(ILogger<HandleSolarium> logger,
            ITelegramBotClient botClient)
                : base(logger, botClient)
        { }

        public async Task BotOnSolarium(Message message)
        {
            _logger.LogInformation("Receive message type in solarium: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;
        }
    }
}
