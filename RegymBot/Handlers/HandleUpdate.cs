using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Helpers.Buttons;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers
{
    public class HandleUpdate
    {
        private readonly ITelegramBotClient _botClient;
        private readonly CallbackQuery _callbackQueryService;
        private readonly HandleError _handleError;
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly ILogger<HandleUpdate> _logger;

        public HandleUpdate(ITelegramBotClient botClient,
            CallbackQuery callbackQueryService,
            HandleError handleError,
            StaticMessageRepository staticMessageRepository,
            ILogger<HandleUpdate> logger)
        {
            _botClient = botClient;
            _callbackQueryService = callbackQueryService;
            _handleError = handleError;
            _staticMessageRepository = staticMessageRepository;
            _logger = logger;
        }

        public async Task EchoAsync(Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update.Message!),
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

        private async Task BotOnMessageReceived(Message message)
        {
            _logger.LogInformation("Receive message type: {MessageType}", message.Type);
            if (message.Type != MessageType.Text)
                return;

            var text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.StartPage);

            await _botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: StartButtons.Buttons);
        }
    }
}
