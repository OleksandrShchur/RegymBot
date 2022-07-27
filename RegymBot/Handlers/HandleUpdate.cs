using Microsoft.Extensions.Logging;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Handlers.ClubList;
using RegymBot.Handlers.Feedback;
using RegymBot.Handlers.MainMenu;
using RegymBot.Handlers.Massage;
using RegymBot.Handlers.Price;
using RegymBot.Handlers.Solarium;
using RegymBot.Helpers.StateContext;
using RegymBot.Services;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Handlers
{
    public class HandleUpdate
    {
        private readonly CallbackQueryMainMenu _mainMenuService;
        private readonly HandleError _handleError;
        private readonly ILogger<HandleUpdate> _logger;
        private readonly HandleMainMenu _handleMainMenu;
        private readonly IStepService _stepService;
        private readonly HandleClubList _handleClubList;
        private readonly CallbackQueryClubList _clubListService;
        private readonly HandleClubContacts _handleClubContacts;
        private readonly CallbackQueryClubContacts _callbackQueryClubContacts;
        private readonly HandleMassage _handleMassage;
        private readonly CallbackQueryMassage _callbackQueryMassage;
        private readonly HandlePrice _handlePrice;
        private readonly CallbackQueryPrice _callbackQueryPrice;
        private readonly HandleSolarium _handleSolarium;
        private readonly CallbackQuerySolarium _callbackQuerySolarium;
        private readonly HandleFeedback _handleFeedback;
        private readonly CallbackQueryFeedback _callbackQueryFeedback;

        public HandleUpdate(
            CallbackQueryMainMenu mainMenuService,
            HandleError handleError,
            ILogger<HandleUpdate> logger,
            HandleMainMenu handleMainMenu,
            IStepService stepService,
            HandleClubList handleClubList,
            CallbackQueryClubList clubListService,
            HandleClubContacts handleClubContacts,
            CallbackQueryClubContacts callbackQueryClubContacts,
            HandleMassage handleMassage,
            CallbackQueryMassage callbackQueryMassage,
            HandlePrice handlePrice,
            CallbackQueryPrice callbackQueryPrice,
            HandleSolarium handleSolarium,
            CallbackQuerySolarium callbackQuerySolarium,
            HandleFeedback handleFeedback,
            CallbackQueryFeedback callbackQueryFeedback)
        {
            _mainMenuService = mainMenuService;
            _handleError = handleError;
            _logger = logger;
            _handleMainMenu = handleMainMenu;
            _stepService = stepService;
            _handleClubList = handleClubList;
            _clubListService = clubListService;
            _handleClubContacts = handleClubContacts;
            _callbackQueryClubContacts = callbackQueryClubContacts;
            _handleMassage = handleMassage;
            _callbackQueryMassage = callbackQueryMassage;
            _handlePrice = handlePrice;
            _callbackQueryPrice = callbackQueryPrice;
            _handleSolarium = handleSolarium;
            _callbackQuerySolarium = callbackQuerySolarium;
            _handleFeedback = handleFeedback;
            _callbackQueryFeedback = callbackQueryFeedback;
        }

        public async Task EchoAsync(Update update)
        {
            var userId = GetUserId(update);
            if (userId == -1)
            {
                return;
            }

            var step = _stepService.GetLastStep(userId);

            switch (step)
            {
                case BotStep.MainMenu:
                    var handler = update.Type switch
                    {
                        UpdateType.Message => _handleMainMenu.BotOnMainMenu(update.Message),
                        UpdateType.CallbackQuery => _mainMenuService.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotStep.Massage:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleMassage.BotOnMassage(update.Message),
                        UpdateType.CallbackQuery => _callbackQueryMassage.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotStep.Solarium:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleSolarium.BotOnSolarium(update.Message),
                        UpdateType.CallbackQuery => _callbackQuerySolarium.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotStep.ClubList:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleClubList.BotOnClubList(update.Message),
                        UpdateType.CallbackQuery => _clubListService.BotOnCallbackQueryClubListReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotStep.LeaveFeedback:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleFeedback.BotOnFeedback(update.Message),
                        UpdateType.CallbackQuery => _callbackQueryFeedback.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotStep.ClubContacts:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleClubContacts.BotOnClubContacts(update.Message),
                        UpdateType.CallbackQuery => _callbackQueryClubContacts.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotStep.Price:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handlePrice.BotOnPrice(update.Message),
                        UpdateType.CallbackQuery => _callbackQueryPrice.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;
            };
        }

        private async Task ExecuteHandler(Task handler)
        {
            try
            {
                await handler;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception in {typeof(HandleUpdate)}, message: {e.Message}, stacktrace: {e.StackTrace}");
                await _handleError.HandleErrorAsync(e);
            }
        }

        private long GetUserId(Update update)
        {
            if (update.ChatMember != null)
            {
                return update.ChatMember.From.Id;
            }
            else if (update.MyChatMember != null)
            {
                return update.MyChatMember.From.Id;
            }
            else if (update.CallbackQuery != null)
            {
                return update.CallbackQuery.From.Id;
            }
            else if (update.InlineQuery != null)
            {
                return update.InlineQuery.From.Id;
            }
            else if (update.Message != null)
            {
                return update.Message.From.Id;
            }
            else if (update.ChosenInlineResult != null)
            {
                return update.ChosenInlineResult.From.Id;
            }

            return -1;
        }
    }
}
