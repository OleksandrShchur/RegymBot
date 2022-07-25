using Microsoft.Extensions.Logging;
using RegymBot.Handlers.ClubList;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.ClubContacts
{
    public class CallbackQueryClubContacts : BaseCallback<CallbackQueryClubContacts>
    {
        private readonly HandleClubList _handleClubList;

        public CallbackQueryClubContacts(
            ITelegramBotClient botClient,
            ILogger<CallbackQueryClubContacts> logger,
            HandleClubList handleClubList,
            IStepService stepService) : base(stepService, botClient, logger)
        {
            _handleClubList = handleClubList;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in club contacts from: {CallQueryFromId}", callbackQuery.From.Id);

            switch (callbackQuery.Data)
            {
                case "back":
                    await _handleClubList.BotOnClubList(callbackQuery.Message);
                    _stepService.ReturnBackStep();

                    break;
            }
        }
    }
}
