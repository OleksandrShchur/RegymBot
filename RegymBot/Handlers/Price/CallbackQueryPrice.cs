using Microsoft.Extensions.Logging;
using RegymBot.Handlers.MainMenu;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.Price
{
    public class CallbackQueryPrice : BaseCallback<CallbackQueryPrice>
    {
        private readonly HandleMainMenu _handleMainMenu;

        public CallbackQueryPrice(ITelegramBotClient botClient,
             ILogger<CallbackQueryPrice> logger,
             IStepService stepService,
             HandleMainMenu handleMainMenu) : base(stepService, botClient, logger)
        {
            _handleMainMenu = handleMainMenu;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in price from: {CallQueryFromId}", callbackQuery.From.Id);
            string text;

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
