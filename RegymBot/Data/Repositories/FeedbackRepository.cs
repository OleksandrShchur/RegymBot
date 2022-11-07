using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class FeedbackRepository : BaseRepository<FeedbackEntity>
    {
        public FeedbackRepository(AppDbContext context, ILogger<FeedbackRepository> logger)
            : base(context, logger) { }

        public async Task AddNewFeedbackAsync(string text, long userId, string fullName, string tgLogin)
        {
            try
            {
                var feedback = new FeedbackEntity
                {
                    Feedback = text,
                    DateCreated = DateTime.Now.ToUniversalTime(),
                    FullName = fullName,
                    TelegramLogin = tgLogin,
                    UserId = userId
                };

                await Insert(feedback);
                _logger.LogInformation($"Wrote new record in feedback table from userId: {userId}");
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on adding new feedback {typeof(FeedbackEntity)}");
                throw;
            }
        }

        public async Task<IEnumerable<FeedbackEntity>> LoadAllFeedbacksAsync()
        {
            try
            {
                var feedbacks = await _context.Feedbacks.ToListAsync();

                _logger.LogInformation("Loaded feedbacks table from userId");

                return feedbacks;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on get all {typeof(FeedbackEntity)}");
                throw;
            }
        }
    }
}
