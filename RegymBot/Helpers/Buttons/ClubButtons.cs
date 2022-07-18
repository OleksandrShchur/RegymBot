using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class ClubButtons
    {
        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
        new[]
            {
                // 1 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("REGYM Аполло", "club"),
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("REGYM Вавилон", "club"),
            },
            // 3 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("REGYM PSHKN", "club"),
            },
            MainMenuButton.Button
        });
    }
}
