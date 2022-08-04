using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Handlers.MainMenu;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.CategorySection
{
    public class CallbackQueryCategorySection : BaseCallback<CallbackQueryCategorySection>
    {
        private readonly HandleClubContacts _handleClubContacts;
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly HandleMainMenu _handleMainMenu;

        public CallbackQueryCategorySection(
            ITelegramBotClient botClient,
            ILogger<CallbackQueryCategorySection> logger,
            IStepService stepService,
            HandleClubContacts handleClubContacts,
            StaticMessageRepository staticMessageRepository,
            HandleMainMenu handleMainMenu) : base(stepService, botClient, logger)
        {
            _handleClubContacts = handleClubContacts;
            _staticMessageRepository = staticMessageRepository;
            _handleMainMenu = handleMainMenu;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in category section from: {CallQueryFromId}", callbackQuery.From.Id);
            string text;

            switch (callbackQuery.Data)
            {
                case "back":
                    _stepService.ReturnBackStep(callbackQuery.From.Id);
                    await _handleClubContacts.BotOnClubContacts(callbackQuery.Message);

                    break;

                case "back_search":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.CategoryPage);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.From.Id,
                                                    text: text,
                                                    replyMarkup: CategoryButtons.Keyboard);

                    break;

                case "main_menu":
                    _stepService.ToStartPage(callbackQuery.From.Id);
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.StartPage);

                    await _botClient.SendChatActionAsync(callbackQuery.From.Id, ChatAction.Typing);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.From.Id,
                                                            text: text,
                                                            replyMarkup: StartButtons.Keyboard);

                    break;
            }
        }
    }
}
