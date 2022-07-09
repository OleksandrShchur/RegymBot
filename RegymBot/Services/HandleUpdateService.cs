using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RegymBot.Services
{
    public class HandleUpdateService
    {
        private readonly ITelegramBotClient _botClient;

        public HandleUpdateService(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        public async Task EchoAsync(Update update)
        {

        }
    }
}
