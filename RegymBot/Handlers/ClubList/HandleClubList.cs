using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Helpers.Buttons;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.ClubList
{
    public class HandleClubList : BaseHandle<HandleClubList>
    {
        private readonly StaticMessageRepository _staticMessageRepository;

        public HandleClubList(ILogger<HandleClubList> logger,
            ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository)
                : base(logger, botClient)
        {
            _staticMessageRepository = staticMessageRepository;
        }

        public async Task BotOnClubList(Message message)
        {
            _logger.LogInformation("Receive message type in club list: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;

            string text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.SelectClub);

            await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                            text: text,
                                            replyMarkup: ClubButtons.Keyboard);
        }
    }
}
