using Microsoft.Extensions.Logging;
using RegymBot.Services;
using Telegram.Bot;

namespace RegymBot.Handlers
{
    public class BaseCallback<T>
        where T: class
    {
        protected readonly IStepService _stepService;
        protected readonly ITelegramBotClient _botClient;
        protected readonly ILogger<T> _logger;

        public BaseCallback(IStepService stepService, ITelegramBotClient botClient, ILogger<T> logger)
        {
            _stepService = stepService;
            _botClient = botClient;
            _logger = logger;
        }
    }
}
