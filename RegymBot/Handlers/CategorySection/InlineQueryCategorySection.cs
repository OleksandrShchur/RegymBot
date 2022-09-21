using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Helpers;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            IConfiguration configuration
        ) : base(stepService, botClient, logger)
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
            var club = _stepService.SelectedClub(inlineQuery.From.Id);

            var searchQueryLen = inlineQuery.Query.Split(" ").Length;
            var searchQuery = inlineQuery.Query.Split(" ");

            var coaches = new List<UserEntity>();
            var coachesQuery = (await _userRepository.LoadCoachesQuery()).Where(u => u.Category == category);
            
            if (club != RegymClub.None) 
            { 
                coachesQuery = coachesQuery.Where(u => u.UserClubs.Any(ur => ur.ClubRef == (int) club));
            }

            if (searchQueryLen > 2)
            {
                coachesQuery = coachesQuery.Where(s => ($"{s.Name} {s.Surname}").ToLower().Contains(searchQuery[2]));
            }

            try 
            {
                coaches = await coachesQuery.ToListAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error on get coachs by category");
                throw;
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

            await _botClient.AnswerInlineQueryAsync(inlineQuery.Id, list.ToArray(), 100, false);
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

            var caption = $"{coach.Name} {coach.Surname}\n\n{coach.Description}";
            if (caption.Length > 1000) { 
                var lastLine = caption.LastIndexOf("\n", 1000);
                lastLine = lastLine == -1 ? 1000 : lastLine;
                var captionPart1 = caption.Substring(0, lastLine);
                var captionPart2 = caption.Substring(lastLine, caption.Length - lastLine);
                await _botClient.SendPhotoAsync(chatId: message.Chat.Id,
                    photo: $"{imgPath}/avatars/{coach.UserGuid.ToString()}.jpg?a={DateTime.UtcNow.ToString("s")}",
                    caption: captionPart1
                );
                await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: captionPart2,
                    replyMarkup: CoachButtons.Keyboard
                );
                return;
            }

            await _botClient.SendPhotoAsync(chatId: message.Chat.Id,
                photo: $"{imgPath}/avatars/{coach.UserGuid.ToString()}.jpg?a={DateTime.UtcNow.ToString("s")}",
                caption: $"{coach.Name} {coach.Surname}\n\n" + coach.Description,
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
