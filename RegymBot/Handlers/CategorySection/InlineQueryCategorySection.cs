using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Helpers.Buttons;
using RegymBot.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InlineQueryResults;

namespace RegymBot.Handlers.CategorySection
{
    public class InlineQueryCategorySection : BaseCallback<InlineQueryCategorySection>
    {
        private readonly HandleClubContacts _handleClubContacts;
        private readonly UserRepository _userRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        public InlineQueryCategorySection(
            ITelegramBotClient botClient,
            ILogger<InlineQueryCategorySection> logger,
            HandleClubContacts handleClubContacts,
            IStepService stepService,
            UserRepository userRepository,
            IWebHostEnvironment appEnvironment) : base(stepService, botClient, logger)
        {
            _handleClubContacts = handleClubContacts;
            _userRepository = userRepository;
            _appEnvironment = appEnvironment;
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

            Random rnd = new Random();

            foreach (var coach in coaches)
            {
                var item = new InlineQueryResultArticle(coach.UserGuid.ToString(),
                    $"{coach.Name} {coach.Surname}",
                    new InputTextMessageContent(coach.Description));

                // TODO
                //FileStream file = new FileStream($"{_appEnvironment.WebRootPath}\\{rnd.Next(1, 9)}.jpg", FileMode.Open);
                //var image = Image.FromStream(file);
                //var thumb = image.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);

                //item.ThumbWidth = 50;
                //item.ThumbHeight = 50;
                //item.ThumbUrl = $"{_appEnvironment.WebRootPath}\\{rnd.Next(1, 9)}.jpg";


                item.Description = coach.Description;
                item.ReplyMarkup = CoachButtons.Keyboard;

                list.Add(item);
            }

            await _botClient.AnswerInlineQueryAsync(inlineQuery.Id, list.ToArray(), null, false);
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
