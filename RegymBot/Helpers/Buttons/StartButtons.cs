using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class StartButtons
    {
        public static InlineKeyboardMarkup Buttons = new InlineKeyboardMarkup(
        new[]{
            // 1 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Выбрать клуб", "club"),
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Массаж", "massage"),
                InlineKeyboardButton.WithCallbackData("Солярий", "solarium"),
            },
            // 3 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Прайс", "price"),
                InlineKeyboardButton.WithCallbackData("Акции", "sale"),
            },
            // 4 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Наши соцсети", "social"),
            },
            // 5 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("Оставить отзыв", "feedback"),
            }
        });
    }
}
