using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Handlers.MainMenu;
using RegymBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.TrainingSchedule
{
    public class CallbackQueryTrainingSchedule : BaseCallback<CallbackQueryTrainingSchedule>
    {
        private readonly HandleClubContacts _handleClubContacts;
        private readonly HandleMainMenu _handleMainMenu;
        public CallbackQueryTrainingSchedule(ITelegramBotClient botClient,
             ILogger<CallbackQueryTrainingSchedule> logger,
             IStepService stepService,
             HandleClubContacts handleClubContacts,
             HandleMainMenu handleMainMenu) : base(stepService, botClient, logger)
        {
            _handleClubContacts = handleClubContacts;
            _handleMainMenu = handleMainMenu;
        }

        public async Task BotOnCallbackQueryReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in training schedule from: {CallQueryFromId}", callbackQuery.From.Id);

            switch (callbackQuery.Data)
            {
                case "back":
                    _stepService.ReturnBackStep(callbackQuery.From.Id);
                    await _handleClubContacts.BotOnClubContacts(callbackQuery.Message);

                    break;

                case "main_menu":
                    _stepService.ToStartPage(callbackQuery.From.Id);
                    await _handleMainMenu.BotOnMainMenu(callbackQuery.Message);

                    break;
            }
        }
    }
}
