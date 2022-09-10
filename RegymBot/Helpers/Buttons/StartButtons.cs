using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class StartButtons
    {
        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
        new[]{
            // 1 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Обрати клуб 🦾", "select_club"),
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Массаж 👏🏻", "massage"),
                InlineKeyboardButton.WithCallbackData("Солярій ☀️", "solarium"),
            },
            // 3 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Прайс 💲", "price"),
                InlineKeyboardButton.WithCallbackData("Акції 🎉", "sale"),
            },
            // 4 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Наші соцмережі 📱", "social"),
            },
            // 5 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Залишити відгук 👍🏻", "feedback"),
            }
        });
    }
}
