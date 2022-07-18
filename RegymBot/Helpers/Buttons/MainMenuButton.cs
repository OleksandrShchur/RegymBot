using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class MainMenuButton
    {
        public static InlineKeyboardButton[] Button => new[]
        {
            InlineKeyboardButton.WithCallbackData("У головне меню", "main_menu"),
        };

        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
        new[]
        {
            Button
        });
    }
}
