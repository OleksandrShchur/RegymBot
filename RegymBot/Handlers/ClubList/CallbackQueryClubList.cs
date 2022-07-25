using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.MainMenu;
using RegymBot.Helpers;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.ClubList
{
    public class CallbackQueryClubList : BaseCallback<CallbackQueryClubList>
    {
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly HandleMainMenu _handleMainMenu;

        public CallbackQueryClubList(ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository,
            ILogger<CallbackQueryClubList> logger,
            IStepService stepService,
            HandleMainMenu handleMainMenu) : base(stepService, botClient, logger)
        {
            _staticMessageRepository = staticMessageRepository;
            _handleMainMenu = handleMainMenu;
        }

        public async Task BotOnCallbackQueryClubListReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in club list from: {CallQueryFromId}", callbackQuery.From.Id);
            string text;

            switch (callbackQuery.Data)
            {
                case "club":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.ContactClubPage);
                    _stepService.NewStep(BotStep.ClubContacts);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard);

                    break;

                case "back":
                    _stepService.ReturnBackStep();
                    await _handleMainMenu.BotOnMainMenu(callbackQuery.Message);

                    break;
            }
        }
    }
}
