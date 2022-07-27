using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class ReturnBackButton
    {
        public static InlineKeyboardButton[] Button => new[]
        {
            InlineKeyboardButton.WithCallbackData("Назад", "back"),
        };

        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
        new[]
        {
            Button
        });
    }
}
