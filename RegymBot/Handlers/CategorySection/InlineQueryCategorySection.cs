using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InlineQueryResults;

namespace RegymBot.Handlers.CategorySection
{
    public class InlineQueryCategorySection : BaseCallback<InlineQueryCategorySection>
    {
        private readonly HandleClubContacts _handleClubContacts;
        private readonly UserRepository _userRepository;
        public InlineQueryCategorySection(
            ITelegramBotClient botClient,
            ILogger<InlineQueryCategorySection> logger,
            HandleClubContacts handleClubContacts,
            IStepService stepService,
            UserRepository userRepository) : base(stepService, botClient, logger)
        {
            _handleClubContacts = handleClubContacts;
            _userRepository = userRepository;
        }

        public async Task BotOnInlineQueryReceived(Telegram.Bot.Types.InlineQuery inlineQuery)
        {
            _logger.LogInformation("Received inline query in category section from: {InlineQueryFromId}", inlineQuery.From.Id);

            var list = new List<InlineQueryResultArticle>();
            var category = DetectCategoryFromQuery(inlineQuery.Query);

            var coaches = _userRepository.LoadCoachesByCategory(category);

            foreach(var coach in coaches)
            {
                var item = new InlineQueryResultArticle(coach.UserGuid.ToString(),
                    $"{coach.Name} {coach.Surname}",
                    new InputTextMessageContent(coach.Description));

                item.ReplyMarkup = CoachButtons.Keyboard;

                list.Add(item);
            }

            await _botClient.AnswerInlineQueryAsync(inlineQuery.Id, list.ToArray(), null, false);
        }

        private Category DetectCategoryFromQuery(string query)
        {
            if (query == "category: vip")
            {
                return Category.VIP;
            }
            else if (query == "category: first")
            {
                return Category.First;
            }
            else
            {
                return Category.Second;
            }
        }
    }
}
