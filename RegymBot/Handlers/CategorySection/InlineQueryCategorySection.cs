using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Helpers;
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
        private IConfiguration Configuration { get; }

        private readonly HandleClubContacts _handleClubContacts;
        private readonly UserRepository _userRepository;
        
        public InlineQueryCategorySection(
            ITelegramBotClient botClient,
            ILogger<InlineQueryCategorySection> logger,
            HandleClubContacts handleClubContacts,
            IStepService stepService,
            UserRepository userRepository,
            IConfiguration configuration) : base(stepService, botClient, logger)
        {
            _handleClubContacts = handleClubContacts;
            _userRepository = userRepository;
            Configuration = configuration;
        }

        public async Task BotOnInlineQueryReceived(Telegram.Bot.Types.InlineQuery inlineQuery)
        {
            _logger.LogInformation("Received inline query in category section from: {InlineQueryFromId}", inlineQuery.From.Id);

            var list = new List<InlineQueryResultArticle>();
            var category = DetectCategoryFromQuery(inlineQuery.Query);

            var searchQueryLen = inlineQuery.Query.Split(" ").Length;
            var searchQuery = inlineQuery.Query.Split(" ");

            var coaches = await _userRepository.LoadCoachesByCategoryAsync(category);
            if (searchQueryLen > 2)
            {
                coaches = coaches.FindAll(s => ($"{s.Name} {s.Surname}").ToLower().Contains(searchQuery[2]));
            }

            string imgPath = Configuration.GetSection("BotConfiguration")
                .Get<BotConfiguration>()
                .HostAddress;

            foreach (var coach in coaches)
            {
                var item = new InlineQueryResultArticle(coach.UserGuid.ToString(),
                    $"{coach.Name} {coach.Surname}",
                    new InputTextMessageContent(coach.UserGuid.ToString()))
                {
                    ThumbUrl = imgPath + "/avatars/" + coach.UserGuid.ToString() + ".jpg",
                    Description = coach.Description,
                };

                list.Add(item);
            }

            await _botClient.AnswerInlineQueryAsync(inlineQuery.Id, list.ToArray(), null, false);
        }

        public async Task BotOnInlineQueryAnswerReceived(Telegram.Bot.Types.Message message)
        {
            _logger.LogInformation("Received inline query answer in category section from: {InlineQueryFromId}", message.From.Id);


            string imgPath = Configuration.GetSection("BotConfiguration")
                .Get<BotConfiguration>()
                .HostAddress;

            var coach = await _userRepository.Entities.FirstOrDefaultAsync(c => c.UserGuid.ToString() == message.Text);

            if (coach == null)
            {
                await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: $"Не можу знайти тренера за заданим пошуком 😓",
                    replyMarkup: CoachButtons.Keyboard
                );
                return;
            }

            await _botClient.SendPhotoAsync(chatId: message.Chat.Id,
                photo: imgPath + "/avatars/" + coach.UserGuid.ToString() + ".jpg",
                caption: $"{coach.Name} {coach.Surname}/n/n" + coach.Description,
                replyMarkup: CoachButtons.Keyboard
            );
        }

        private Category? DetectCategoryFromQuery(string query)
        {
            if (query.Contains("category: vip"))
            {
                return Category.VIP;
            }
            else if (query.Contains("category: first"))
            {
                return Category.First;
            }
            else if (query.Contains("category: second"))
            {
                return Category.Second;
            }

            return null;
        }
    }
}
