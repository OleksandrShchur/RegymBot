using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.ClubList;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.ClubContacts
{
    public class CallbackQueryClubContacts : BaseCallback<CallbackQueryClubContacts>
    {
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly HandleClubList _handleClubList;

        public CallbackQueryClubContacts(
            ITelegramBotClient botClient,
            ILogger<CallbackQueryClubContacts> logger,
            StaticMessageRepository staticMessageRepository,
            HandleClubList handleClubList,
            IStepService stepService) : base(stepService, botClient, logger)
        {
            _staticMessageRepository = staticMessageRepository;
            _handleClubList = handleClubList;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in club contacts from: {CallQueryFromId}", callbackQuery.From.Id);
            string text;

            switch (callbackQuery.Data)
            {
                case "coach":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.CategoryPage);
                    _stepService.NewStep(BotPage.CategoryPage, callbackQuery.From.Id);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: CategoryButtons.Keyboard);

                    break;

                case "back":
                    _stepService.ReturnBackStep(callbackQuery.From.Id);
                    await _handleClubList.BotOnClubList(callbackQuery.Message);

                    break;
            }
        }
    }
}
