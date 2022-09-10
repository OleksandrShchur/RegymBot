using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class ReturnBackButton
    {
        public static InlineKeyboardButton[] BackButton => new[]
        {
            InlineKeyboardButton.WithCallbackData("Назад ↩️", "back"),
        };

        public static InlineKeyboardButton[] HomeButton => new[]
        {
            InlineKeyboardButton.WithCallbackData("Головне меню 🚪", "main_menu")
        };

        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
        new[]
        {
            BackButton
        });
    }
}
