using Microsoft.Extensions.Logging;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace RegymBot.Handlers.CategorySection
{
    public class InlineQueryCategorySection : BaseCallback<InlineQueryCategorySection>
    {
        private readonly HandleClubContacts _handleClubContacts;
        public InlineQueryCategorySection(
            ITelegramBotClient botClient,
            ILogger<InlineQueryCategorySection> logger,
            HandleClubContacts handleClubContacts,
            IStepService stepService) : base(stepService, botClient, logger)
        {
            _handleClubContacts = handleClubContacts;
        }

        public async Task BotOnInlineQueryReceived(Telegram.Bot.Types.InlineQuery inlineQuery)
        {
            _logger.LogInformation("Received inline query in category section from: {InlineQueryFromId}", inlineQuery.From.Id);

            var list = new List<InlineQueryResultArticle>();

            var articles = new[]
            {
                new InlineQueryResultArticle("1", "test", new InputTextMessageContent("message context")),
                new InlineQueryResultArticle("2", "test 2", new InputTextMessageContent("message context 2")),
                new InlineQueryResultArticle("3", "test 3", new InputTextMessageContent("message context 3")),
            };
            
            foreach(var article in articles)
            {
                article.ReplyMarkup = CoachButtons.Keyboard;
            }

            list.Add(articles[0]);
            list.Add(articles[1]);
            list.Add(articles[2]);

            await _botClient.AnswerInlineQueryAsync(inlineQuery.Id, list.ToArray(), null, false);
        }
    }
}
