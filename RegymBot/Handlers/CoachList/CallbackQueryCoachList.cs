using Microsoft.Extensions.Logging;
using RegymBot.Handlers.CategorySection;
using RegymBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.CoachList
{
    public class CallbackQueryCoachList : BaseCallback<CallbackQueryCoachList>
    {
        private readonly HandleCategorySection _handleCategorySection;
        public CallbackQueryCoachList(
            ITelegramBotClient botClient,
            ILogger<CallbackQueryCoachList> logger,
            HandleCategorySection handleCategorySection,
            IStepService stepService) : base(stepService, botClient, logger)
        {
            _handleCategorySection = handleCategorySection;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in coach list from: {CallQueryFromId}", callbackQuery.From.Id);

            switch (callbackQuery.Data)
            {
                case "back":
                    await _handleCategorySection.BotOnCategorySection(callbackQuery.Message);
                    _stepService.ReturnBackStep(callbackQuery.From.Id);

                    break;

            }
        }
    }
}
