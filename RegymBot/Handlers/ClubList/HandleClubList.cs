using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.ClubList
{
    public class HandleClubList : BaseHandle<HandleClubList>
    {

        public HandleClubList(ILogger<HandleClubList> logger,
            ITelegramBotClient botClient)
                : base(logger, botClient)
        { }

        public async Task BotOnClubList(Message message)
        {
            _logger.LogInformation("Receive message type in club list: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;
        }
    }
}
