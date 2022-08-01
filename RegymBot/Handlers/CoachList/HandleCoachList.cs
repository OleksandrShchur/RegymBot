using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.CoachList
{
    public class HandleCoachList : BaseHandle<HandleCoachList>
    {
        public HandleCoachList(ILogger<HandleCoachList> logger,
            ITelegramBotClient botClient)
                : base(logger, botClient)
        { }

        public async Task BotOnCoachList(Message message)
        {
            _logger.LogInformation("Receive message type in coach list: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;
        }
    }
}
