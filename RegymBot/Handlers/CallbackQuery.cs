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

        public CallbackQuery(ITelegramBotClient botClient,
            PriceRepository priceRepository,
            StaticMessageRepository staticMessageRepository
            )
        {
            _botClient = botClient;
            _priceRepository = priceRepository;
            _staticMessageRepository = staticMessageRepository;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            string text;

            switch (callbackQuery.Data)
            {
                // start buttons cases
                case "select_club":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.SelectClubPage);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubButtons.Buttons);

                    break;
                case "price":
                    var prices = await _priceRepository.GetAllAsync();
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.PricePage);

                    foreach (PriceEntity price in prices)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text);

                    break;

                case "solarium":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.SolariumPage);
                    var pricesSolarium = await _priceRepository.GetPricesByTypeAsync(PriceItem.Solarium);

                    foreach(PriceEntity price in pricesSolarium)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text);

                    break;

                case "massage":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.MassagePage);
                    var pricesMassage = await _priceRepository.GetPricesByTypeAsync(PriceItem.Massage);

                    foreach(PriceEntity price in pricesMassage)
                    {
                        text += $"\n- {price.PriceName} - {price.Price};";
                    }

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text);

                    break;

                case "feedback":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.LeaveFeedbackPage);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text);

                    break;

                // select club cases
                case "club":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.ContactClubPage);

                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Buttons);

                    break;
            }
        }
    }
}
