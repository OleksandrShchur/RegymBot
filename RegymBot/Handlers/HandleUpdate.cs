using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Handlers.CategorySection;
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
        private readonly ILogger _logger;
        private readonly HandleMainMenu _handleMainMenu;
        private readonly IStepService _stepService;
        private readonly HandleClubList _handleClubList;
        private readonly CallbackQueryClubList _clubListService;
        private readonly HandleClubContacts _handleClubContacts;
        private readonly CallbackQueryClubContacts _callbackQueryClubContacts;
        private readonly CallbackQueryMassage _callbackQueryMassage;
        private readonly CallbackQueryPrice _callbackQueryPrice;
        private readonly CallbackQuerySolarium _callbackQuerySolarium;
        private readonly HandleFeedback _handleFeedback;
        private readonly CallbackQueryFeedback _callbackQueryFeedback;
        private readonly HandleCategorySection _handleCategorySection;
        private readonly InlineQueryCategorySection _inlineQueryCategorySection;
        private readonly CallbackQueryCategorySection _callbackQueryCategorySection;

        public HandleUpdate() { }

        public HandleUpdate(
            CallbackQueryMainMenu mainMenuService,
            HandleError handleError,
            ILogger logger,
            HandleMainMenu handleMainMenu,
            IStepService stepService,
            HandleClubList handleClubList,
            CallbackQueryClubList clubListService,
            HandleClubContacts handleClubContacts,
            CallbackQueryClubContacts callbackQueryClubContacts,
            CallbackQueryMassage callbackQueryMassage,
            CallbackQueryPrice callbackQueryPrice,
            CallbackQuerySolarium callbackQuerySolarium,
            HandleFeedback handleFeedback,
            CallbackQueryFeedback callbackQueryFeedback,
            HandleCategorySection handleCategorySection,
            InlineQueryCategorySection inlineQueryCategorySection,
            CallbackQueryCategorySection callbackQueryCategorySection)
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
            _handleFeedback = handleFeedback;
            _callbackQueryFeedback = callbackQueryFeedback;
            _handleCategorySection = handleCategorySection;
            _inlineQueryCategorySection = inlineQueryCategorySection;
            _callbackQueryCategorySection = callbackQueryCategorySection;
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
                case BotPage.StartPage:
                    var handler = update.Type switch
                    {
                        UpdateType.Message => _handleMainMenu.BotOnMainMenu(update.Message),
                        UpdateType.CallbackQuery => _mainMenuService.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.MassagePage:
                    handler = update.Type switch
                    {
                        UpdateType.CallbackQuery => _callbackQueryMassage.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.SolariumPage:
                    handler = update.Type switch
                    {
                        UpdateType.CallbackQuery => _callbackQuerySolarium.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.SelectClubPage:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleClubList.BotOnClubList(update.Message),
                        UpdateType.CallbackQuery => _clubListService.BotOnCallbackQueryClubListReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.LeaveFeedbackPage:
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

                case BotPage.PricePage:
                    handler = update.Type switch
                    {
                        UpdateType.CallbackQuery => _callbackQueryPrice.BotOnCallbackQueryReceived(update.CallbackQuery),
                        _ => _handleError.UnknownUpdateHandlerAsync(update)
                    };

                    await ExecuteHandler(handler);

                    break;

                case BotPage.CategoryPage:
                    handler = update.Type switch
                    {
                        UpdateType.Message => _handleCategorySection.BotOnCategorySection(update.Message),
                        UpdateType.InlineQuery => _inlineQueryCategorySection.BotOnInlineQueryReceived(update.InlineQuery),
                        UpdateType.CallbackQuery => _callbackQueryCategorySection.BotOnCallbackQueryReceived(update.CallbackQuery),
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
