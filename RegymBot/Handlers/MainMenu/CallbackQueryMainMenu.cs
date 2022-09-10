using Microsoft.Extensions.Logging;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers.MainMenu
{
    public class CallbackQueryMainMenu : BaseCallback<CallbackQueryMainMenu>
    {
        private readonly PriceRepository _priceRepository;
        private readonly StaticMessageRepository _staticMessageRepository;

        public CallbackQueryMainMenu(ITelegramBotClient botClient,
             PriceRepository priceRepository,
             StaticMessageRepository staticMessageRepository,
             ILogger<CallbackQueryMainMenu> logger,
             IStepService stepService) : base(stepService, botClient, logger)
        {
            _priceRepository = priceRepository;
            _staticMessageRepository = staticMessageRepository;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in main menu from: {CallQueryFromId}", callbackQuery.From.Id);
            string text;

            switch (callbackQuery.Data)
            {
                case "select_club":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.SelectClub);
                    _stepService.NewStep(BotPage.SelectClub, callbackQuery.From.Id);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubButtons.Keyboard);

                    break;
                case "price":
                    var prices = await _priceRepository.GetAllAsync();
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Price);
                    _stepService.NewStep(BotPage.Price, callbackQuery.From.Id);

                    foreach (PriceEntity price in prices)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ReturnBackButton.Keyboard);

                    break;

                case "solarium":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Solarium);
                    var pricesSolarium = await _priceRepository.GetPricesByTypeAsync(PriceItem.Solarium);
                    _stepService.NewStep(BotPage.Solarium, callbackQuery.From.Id);

                    foreach (PriceEntity price in pricesSolarium)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ReturnBackButton.Keyboard);

                    break;

                case "massage":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Massage);
                    var pricesMassage = await _priceRepository.GetPricesByTypeAsync(PriceItem.Massage);
                    _stepService.NewStep(BotPage.Massage, callbackQuery.From.Id);

                    foreach (PriceEntity price in pricesMassage)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ReturnBackButton.Keyboard);

                    break;

                case "feedback":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.LeaveFeedback);
                    _stepService.NewStep(BotPage.LeaveFeedback, callbackQuery.From.Id);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ReturnBackButton.Keyboard);

                    break;

                case "social":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Social);
                    _stepService.NewStep(BotPage.Social, callbackQuery.From.Id);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    parseMode: ParseMode.Markdown,
                                                    text: text,
                                                    disableWebPagePreview: true,
                                                    replyMarkup: ReturnBackButton.Keyboard);

                    break;

                default:
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Start);
                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                            text: text,
                                                            replyMarkup: StartButtons.Keyboard);
                    break;
            }
        }
    }
}
