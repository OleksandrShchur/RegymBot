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
                InlineKeyboardButton.WithCallbackData("REGYM Аполло 🌆", "club_apollo"),
            },
            // 2 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("REGYM Вавилон 🏙", "club_vavylon"),
            },
            // 3 row
            new []
            {
                InlineKeyboardButton.WithCallbackData("REGYM PSHKN 🌌", "club_pshkn"),
            },
            ReturnBackButton.BackButton
        });
    }
}
