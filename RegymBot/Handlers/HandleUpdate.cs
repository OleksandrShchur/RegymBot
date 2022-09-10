using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Handlers.AdminCommands;
using RegymBot.Handlers.CategorySection;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Handlers.ClubList;
using RegymBot.Handlers.Feedback;
using RegymBot.Handlers.MainMenu;
using RegymBot.Handlers.Massage;
using RegymBot.Handlers.Price;
using RegymBot.Handlers.Solarium;
using RegymBot.Handlers.TrainingSchedule;
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
        private readonly CallbackQueryMassage _callbackQueryMassage;
        private readonly CallbackQueryPrice _callbackQueryPrice;
        private readonly CallbackQuerySolarium _callbackQuerySolarium;
        private readonly CallbackQuerySocial _callbackQuerySocial;
        private readonly HandleFeedback _handleFeedback;
        private readonly CallbackQueryFeedback _callbackQueryFeedback;
        private readonly InlineQueryCategorySection _inlineQueryCategorySection;
        private readonly CallbackQueryCategorySection _callbackQueryCategorySection;
        private readonly CallbackQueryTrainingSchedule _callbackQueryTrainingSchedule;
        private readonly HandleTrainingSchedule _handleTrainingSchedule;
        private readonly HandleAdminCommands _handleAdminCommands;

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
            CallbackQueryMassage callbackQueryMassage,
            CallbackQueryPrice callbackQueryPrice,
            CallbackQuerySolarium callbackQuerySolarium,
            CallbackQuerySocial callbackQuerySocial,
            HandleFeedback handleFeedback,
            CallbackQueryFeedback callbackQueryFeedback,
            InlineQueryCategorySection inlineQueryCategorySection,
            CallbackQueryCategorySection callbackQueryCategorySection,
            CallbackQueryTrainingSchedule callbackQueryTrainingSchedule,
            HandleTrainingSchedule handleTrainingSchedule,
            HandleAdminCommands handleAdminCommands)
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
            _callbackQueryMassage = callbackQueryMassage;
            _callbackQueryPrice = callbackQueryPrice;
            _callbackQuerySolarium = callbackQuerySolarium;
            _callbackQuerySocial = callbackQuerySocial;
            _handleFeedback = handleFeedback;
            _callbackQueryFeedback = callbackQueryFeedback;
            _inlineQueryCategorySection = inlineQueryCategorySection;
            _callbackQueryCategorySection = callbackQueryCategorySection;
            _callbackQueryTrainingSchedule = callbackQueryTrainingSchedule;
            _handleTrainingSchedule = handleTrainingSchedule;
            _handleAdminCommands = handleAdminCommands;
        }

        public async Task EchoAsync(Update update)
        {
            var userId = GetUserId(update);
            if (userId == -1)
            {
                return;
            }

            await _handleAdminCommands.handleClubToken(update);

            var step = _stepService.GetLastStep(userId);

            switch (step)
            {
                case BotPage.Start:
                    var handler = update.Type switch
                    {
                        UpdateType.Message => _handleMainMenu.BotOnMainMenu(update.Message),
                        UpdateType.CallbackQuery => _mainMenuService.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.Massage:
                    handler = update.Type switch
                    {
                        UpdateType.CallbackQuery => _callbackQueryMassage.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.Solarium:
                    handler = update.Type switch
                    {
                        UpdateType.CallbackQuery => _callbackQuerySolarium.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.Social:
                    handler = update.Type switch
                    {
                        UpdateType.CallbackQuery => _callbackQuerySocial.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.SelectClub:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleClubList.BotOnClubList(update.Message),
                        UpdateType.CallbackQuery => _clubListService.BotOnCallbackQueryClubListReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.LeaveFeedback:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleFeedback.BotOnFeedback(update.Message),
                        UpdateType.CallbackQuery => _callbackQueryFeedback.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.Club_Pshkn:
                case BotPage.Club_Vavylon:
                case BotPage.Club_Apollo:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleClubContacts.BotOnClubContacts(update.Message),
                        UpdateType.CallbackQuery => _callbackQueryClubContacts.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.Price:
                    handler = update.Type switch
                    {
                        UpdateType.CallbackQuery => _callbackQueryPrice.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.Category:
                    handler = update.Type switch
                    {
                        UpdateType.InlineQuery => _inlineQueryCategorySection.BotOnInlineQueryReceived(update.InlineQuery),
                        UpdateType.Message => _inlineQueryCategorySection.BotOnInlineQueryAnswerReceived(update.Message),
                        UpdateType.CallbackQuery => _callbackQueryCategorySection.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.TrainingSchedule:
                case BotPage.GetUserName:
                case BotPage.GetUserPhone:
                case BotPage.FinishEnrolInGroup:
                    handler = update.Type switch
                    {
                        UpdateType.CallbackQuery => _callbackQueryTrainingSchedule.BotOnCallbackQueryReceived(update.CallbackQuery),
                        UpdateType.Message => _handleTrainingSchedule.BotOnTrainingSchedule(update.Message),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;
                default:
                    await ExecuteHandler(_handleMainMenu.BotOnMainMenu(update.Message));
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
