using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public class CategoryButtons
    {
        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
        new[]
            {
                // 1 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("VIP", "vip_group"),
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("1 група", "group_1"),
            },
            // 3 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("2 група", "group_2"),
            },
            ReturnBackButton.Button
        });
    }
}
