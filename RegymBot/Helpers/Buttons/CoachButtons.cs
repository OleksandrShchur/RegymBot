using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public class CoachButtons
    {
        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Повернутись до пошуку 🔎", "back_search")
                },
                ReturnBackButton.HomeButton
            });
    }
}
