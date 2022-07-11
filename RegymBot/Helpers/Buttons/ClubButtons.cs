using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public static class ClubButtons
    {
        public static InlineKeyboardMarkup Buttons = new InlineKeyboardMarkup(
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
        });
    }
}
