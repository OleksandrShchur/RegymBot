using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.CategorySection
{
    public class HandleCategorySection : BaseHandle<HandleCategorySection>
    {
        public HandleCategorySection(ILogger<HandleCategorySection> logger,
            ITelegramBotClient botClient)
                : base(logger, botClient)
        { }

        public async Task BotOnCategorySection(Message message)
        {
            _logger.LogInformation("Receive message type in category: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;
        }
    }
}
