using Microsoft.Extensions.Logging;
using RegymBot.Handlers.MainMenu;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.Massage
{
    public class CallbackQueryMassage : BaseCallback<CallbackQueryMassage>
    {
        private readonly HandleMainMenu _handleMainMenu;

        public CallbackQueryMassage(ITelegramBotClient botClient,
             ILogger<CallbackQueryMassage> logger,
             IStepService stepService,
             HandleMainMenu handleMainMenu) : base(stepService, botClient, logger)
        {
            _handleMainMenu = handleMainMenu;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in massage from: {CallQueryFromId}", callbackQuery.From.Id);

            switch (callbackQuery.Data)
            {
                case "back":
                    _stepService.ReturnBackStep();
                    await _handleMainMenu.BotOnMainMenu(callbackQuery.Message);

                    break;
            }
        }
    }
}
