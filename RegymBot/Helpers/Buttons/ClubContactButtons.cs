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
                InlineKeyboardButton.WithUrl("Чат с администратором", "tg://resolve?domain=butinyevhen"),
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Записаться в группу", "enrol"),
            },
            // 3 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Тренер", "coach"),
            },
            ReturnBackButton.Button
        });
    }
}
