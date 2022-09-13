using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.MainMenu;
using RegymBot.Helpers.Buttons;
using RegymBot.Helpers.StateContext;
using RegymBot.Services;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RegymBot.Handlers.ClubList
{
    public class CallbackQueryClubList : BaseCallback<CallbackQueryClubList>
    {
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly HandleMainMenu _handleMainMenu;
        private readonly AppDbContext _dbContext;

        public CallbackQueryClubList(ITelegramBotClient botClient,
            StaticMessageRepository staticMessageRepository,
            ILogger<CallbackQueryClubList> logger,
            IStepService stepService,
            AppDbContext dbContext,
            HandleMainMenu handleMainMenu) : base(stepService, botClient, logger)
        {
            _staticMessageRepository = staticMessageRepository;
            _dbContext = dbContext;
            _handleMainMenu = handleMainMenu;
        }

        public async Task BotOnCallbackQueryClubListReceived(Telegram.Bot.Types.CallbackQuery callbackQuery)
        {
            _logger.LogInformation("Received callback query in club list from: {CallQueryFromId}", callbackQuery.From.Id);
            string text;

            var adminContacts = await _dbContext.AdminsInfo.AsNoTracking().FirstOrDefaultAsync(i => i.AdminsInfoId == 1);

            switch (callbackQuery.Data)
            {
                case "club_apollo":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Apollo);
                    _stepService.NewStep(BotPage.Club_Apollo, callbackQuery.From.Id);

                    await _botClient.SendLocationAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    latitude: 48.4329629859287,
                                                    longitude: 35.002366815339215);
                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard(adminContacts is null ? string.Empty : adminContacts.AdminApolloLogin));

                    break;

                case "club_vavylon":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Vavylon);
                    _stepService.NewStep(BotPage.Club_Vavylon, callbackQuery.From.Id);

                    await _botClient.SendLocationAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    latitude: 48.4840733808793,
                                                    longitude: 35.06434831533921);
                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard(adminContacts is null ? string.Empty : adminContacts.AdminVavylonLogin));

                    break;

                case "club_pshkn":
                    text = await _staticMessageRepository.GetMessageByTypeAsync(BotPage.Club_Pshkn);
                    _stepService.NewStep(BotPage.Club_Pshkn, callbackQuery.From.Id);

                    await _botClient.SendLocationAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    latitude: 48.46442615498108,
                                                    longitude: 35.049427971165194);
                    await _botClient.SendTextMessageAsync(chatId: callbackQuery.Message.Chat.Id,
                                                    text: text,
                                                    replyMarkup: ClubContactButtons.Keyboard(adminContacts is null ? string.Empty : adminContacts.AdminPSHKNLogin));

                    break;

                case "back":
                    _stepService.ReturnBackStep(callbackQuery.From.Id);
                    await _handleMainMenu.BotOnMainMenu(callbackQuery.Message);

                    break;
            }
        }
    }
}
