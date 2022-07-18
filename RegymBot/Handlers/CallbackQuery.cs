using Microsoft.Extensions.Logging;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Helpers.Buttons;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers
{
    public class CallbackQuery
    {
        private readonly ITelegramBotClient _botClient;
        private readonly PriceRepository _priceRepository;
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly ILogger<CallbackQuery> _logger;
        private readonly HandleMainMenu _handleMainMenu;

        public CallbackQuery(ITelegramBotClient botClient,
            PriceRepository priceRepository,
            StaticMessageRepository staticMessageRepository,
            ILogger<CallbackQuery> logger,
            HandleMainMenu handleMainMenu
            )
        {
            _botClient = botClient;
            _priceRepository = priceRepository;
            _staticMessageRepository = staticMessageRepository;
            _logger = logger;
            _handleMainMenu = handleMainMenu;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query from: {CallQueryFromId}", callbackQuery.From.Id);
            string text;

            switch (callbackQuery.Data)
            {
                // start buttons cases
                case "select_club":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.SelectClubPage);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubButtons.Keyboard);

                    break;
                case "price":
                    var prices = await _priceRepository.GetAllAsync();
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.PricePage);

                    foreach (PriceEntity price in prices)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: MainMenuButton.Keyboard);

                    break;

                case "solarium":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.SolariumPage);
                    var pricesSolarium = await _priceRepository.GetPricesByTypeAsync(PriceItem.Solarium);

                    foreach(PriceEntity price in pricesSolarium)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: MainMenuButton.Keyboard);

                    break;

                case "massage":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.MassagePage);
                    var pricesMassage = await _priceRepository.GetPricesByTypeAsync(PriceItem.Massage);

                    foreach(PriceEntity price in pricesMassage)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: MainMenuButton.Keyboard);

                    break;

                case "feedback":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.LeaveFeedbackPage);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: MainMenuButton.Keyboard);

                    break;

                // select club cases
                case "club":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.ContactClubPage);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard);

                    break;

                // back to main menu
                case "main_menu":
                    await _handleMainMenu.BotOnMainMenu(callbackQuery.Message);

                    break;
            }
        }
    }
}
