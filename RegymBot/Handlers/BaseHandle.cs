using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace RegymBot.Handlers
{
    public class BaseHandle<T>
        where T: class
    {
        protected readonly ILogger<T> _logger;
        protected readonly ITelegramBotClient _botClient;

        public BaseHandle(ILogger<T> logger,
            ITelegramBotClient botClient)
        {
            _logger = logger;
            _botClient = botClient;
        }
    }
}
