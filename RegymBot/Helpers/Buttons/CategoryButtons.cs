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
                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("VIP 🌟", "category: vip ")
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("1 група 1️⃣", "category: first "),
            },
            // 3 row
            new []
            {
                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("2 група 2️⃣", "category: second "),
            },
            ReturnBackButton.BackButton
        });
    }
}
