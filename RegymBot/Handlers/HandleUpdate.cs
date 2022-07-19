using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers
{
    public class HandleUpdate
    {
        private readonly CallbackQuery _callbackQueryService;
        private readonly HandleError _handleError;
        private readonly ILogger _logger;
        private readonly HandleMainMenu _handleMainMeny;

        public HandleUpdate(
            CallbackQuery callbackQueryService,
            HandleError handleError,
            ILogger logger,
            HandleMainMenu handleMainMenu)
        {
            _callbackQueryService = callbackQueryService;
            _handleError = handleError;
            _logger = logger;
            _handleMainMeny = handleMainMenu;
        }

        public async Task EchoAsync(Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => _handleMainMeny.BotOnMainMenu(update.Message!),
                UpdateType.CallbackQuery => _callbackQueryService.BotOnCallbackQueryReceived(update.CallbackQuery!),
                _ => _handleError.UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in {typeof(HandleUpdate)}, message: {e.Message}, stacktrace: {e.StackTrace}");
                await _handleError.HandleErrorAsync(e);
            }
        }
    }
}
