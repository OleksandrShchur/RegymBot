using RegymBot.Helpers.Buttons;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers
{
    public class CallbackQuery
    {
        private readonly ITelegramBotClient _botClient;
        private const string CONTACTS = "Телефон:\n"
            + "+380999999999\n"
            + "Адрес:\n"
            + "ул. Хмельницкого 68\n"
            + "Расписание:\n"
            + "ПН-СБ: 08-21\n"
            + "ВС: 9-18";

        public CallbackQuery(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            switch (callbackQuery.Data)
            {
                // start buttons cases
                case "select_club":
                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: "Наши клубы:",
                                                    replyMarkup: ClubButtons.Buttons);

                    break;
                // select club cases
                case "club":
                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: CONTACTS,
                                                    replyMarkup: ClubContactButtons.Buttons);

                    break;
            }
        }
    }
}
