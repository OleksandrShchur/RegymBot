using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class ClubContactButtons
    {
        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
        new[]
            {
                // 1 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Чат с администратором", "chat_with_admin"),
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Записаться в группу", "enrol"),
            },
            MainMenuButton.Button
        });
    }
}
