using Microsoft.Extensions.Logging;
using RegymBot.Data.Repositories;
using RegymBot.Helpers.Buttons;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.Feedback
{
    public class HandleFeedback : BaseHandle<HandleFeedback>
    {
        private readonly FeedbackRepository _feedbackRepository;
        public HandleFeedback(ILogger<HandleFeedback> logger,
            ITelegramBotClient botClient,
            FeedbackRepository feedbackRepository)
                : base(logger, botClient)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task BotOnFeedback(Message message)
        {
            _logger.LogInformation("Receive message type in massage: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;

            await _feedbackRepository.AddNewFeedbackAsync(message.Text, message.From.Id, $"{message.From.FirstName} {message.From.LastName}", message.From.Username);

            await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: "Спасибі за ваш відгук!\n",
                                                    replyMarkup: ReturnBackButton.Keyboard);
        }
    }
}
