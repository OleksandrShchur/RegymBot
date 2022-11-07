using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class ClubContactButtons
    {
        public static InlineKeyboardMarkup Keyboard(string adminLogin) => new InlineKeyboardMarkup(
        new[]
            {
            // 1 row
            new []
            {
                InlineKeyboardButton.WithUrl("Чат с администратором 💬", $"tg://resolve?domain={adminLogin}"),
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Прайс 💲", "price"),
                InlineKeyboardButton.WithCallbackData("Тренер 🏋️", "coach"),
            },
            // 3 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Записаться в группу 📝", "training_schedule"),
            },
            ReturnBackButton.BackButton
        });
    }
}
