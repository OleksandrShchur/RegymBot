using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.ClubContacts
{
    public class HandleClubContacts : BaseHandle<HandleClubContacts>
    {

        public HandleClubContacts(ILogger<HandleClubContacts> logger,
            ITelegramBotClient botClient)
                : base(logger, botClient)
        { }

        public async Task BotOnClubContacts(Message message)
        {
            _logger.LogInformation("Receive message type in club contacts: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;
        }
    }
}
