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

        public HandleUpdate(ITelegramBotClient botClient,
            CallbackQuery callbackQueryService,
            HandleError handleError,
            StaticMessageRepository staticMessageRepository)
        {
            _botClient = botClient;
            _callbackQueryService = callbackQueryService;
            _handleError = handleError;
            _staticMessageRepository = staticMessageRepository;
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
            catch (Exception exception)
            {
                await _handleError.HandleErrorAsync(exception);
            }
        }

        private async Task BotOnMessageReceived(Message message)
        {
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
