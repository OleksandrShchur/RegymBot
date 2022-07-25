using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Helpers.Buttons;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.MainMenu
{
    public class HandleMainMenu : BaseHandle<HandleMainMenu>
    {
        private readonly StaticMessageRepository _staticMessageRepository;

        public HandleMainMenu(ILogger<HandleMainMenu> logger,
            ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository)
                : base(logger, botClient)
        {
            _staticMessageRepository = staticMessageRepository;
        }

        public async Task BotOnMainMenu(Message message)
        {
            _logger.LogInformation("Receive message type in main menu: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;

            var text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.StartPage);

            await _botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: StartButtons.Keyboard);
        }
    }
}
