using Microsoft.Extensions.Logging;
using RegymBot.Handlers.MainMenu;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.Solarium
{
    public class CallbackQuerySocial : BaseCallback<CallbackQuerySocial>
    {
        private readonly HandleMainMenu _handleMainMenu;

        public CallbackQuerySocial(ITelegramBotClient botClient,
             ILogger<CallbackQuerySocial> logger,
             IStepService stepService,
             HandleMainMenu handleMainMenu) : base(stepService, botClient, logger)
        {
            _handleMainMenu = handleMainMenu;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in social from: {CallQueryFromId}", callbackQuery.From.Id);

            switch (callbackQuery.Data)
            {
                case "back":
                    _stepService.ReturnBackStep(callbackQuery.From.Id);
                    await _handleMainMenu.BotOnMainMenu(callbackQuery.Message);

                    break;
            }
        }
    }
}
