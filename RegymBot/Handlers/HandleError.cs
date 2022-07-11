using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace RegymBot.Handlers
{
    public class HandleError
    {
        private readonly ITelegramBotClient _botClient;

        public HandleError(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleErrorAsync(Exception exception)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
        }

        public async Task UnknownUpdateHandlerAsync(Update update)
        {
            var message = "An unknown message received.";
        }
    }
}
