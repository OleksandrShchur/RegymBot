using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Helpers.Buttons
{
    public class EnrolInGroupButtons
    {
        public static InlineKeyboardMarkup Keyboard => new InlineKeyboardMarkup(
        new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Головне меню", "main_menu")
            }
        });
    }
}
