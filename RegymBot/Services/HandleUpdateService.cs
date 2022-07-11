using RegymBot.Helpers.Buttons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace RegymBot.Services
{
    public class HandleUpdateService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly CallbackQueryService _callbackQueryService;

        public HandleUpdateService(ITelegramBotClient botClient, CallbackQueryService callbackQueryService)
        {
            _botClient = botClient;
            _callbackQueryService = callbackQueryService;
        }

        public async Task EchoAsync(Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update.Message!),
                UpdateType.CallbackQuery => _callbackQueryService.BotOnCallbackQueryReceived(update.CallbackQuery!),
                _ => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(exception);
            }
        }

        private async Task BotOnMessageReceived(Message message)
        {
            if (message.Type != MessageType.Text)
                return;


            await _botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                    text: "Выберите:",
                                                    replyMarkup: StartButtons.Buttons);
        }

        private async Task BotOnInlineQueryReceived(InlineQuery inlineQuery)
        {
            object[] results = {
                // displayed result
                new InlineQueryResultArticle(
                    id: "3",
                    title: "TgBots",
                    inputMessageContent: new InputTextMessageContent(
                        "hello"
                    )
                )
            };

            await _botClient.AnswerInlineQueryAsync(inlineQueryId: inlineQuery.Id,
                                                    results: (IEnumerable<InlineQueryResult>)results,
                                                    isPersonal: true,
                                                    cacheTime: 0);
        }

        private Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult)
        {
            return Task.CompletedTask;
        }

        private Task UnknownUpdateHandlerAsync(Update update)
        {
            return Task.CompletedTask;
        }

        public Task HandleErrorAsync(Exception exception)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            return Task.CompletedTask;
        }
    }
}
