using Microsoft.Extensions.Logging;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.CategorySection
{
    public class CallbackQueryCategorySection : BaseCallback<CallbackQueryCategorySection>
    {
        private readonly HandleClubContacts _handleClubContacts;

        public CallbackQueryCategorySection(
            ITelegramBotClient botClient,
            ILogger<CallbackQueryCategorySection> logger,
            IStepService stepService,
            HandleClubContacts handleClubContacts) : base(stepService, botClient, logger)
        {
            _handleClubContacts = handleClubContacts;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in category section from: {CallQueryFromId}", callbackQuery.From.Id);

            switch (callbackQuery.Data)
            {
                case "back":
                    _stepService.ReturnBackStep(callbackQuery.From.Id);
                    await _handleClubContacts.BotOnClubContacts(callbackQuery.Message);

                    break;
            }
        }
    }
}
