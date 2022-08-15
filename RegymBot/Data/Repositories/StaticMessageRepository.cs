using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class StaticMessageRepository : BaseRepository<StaticMessageEntity>
    {
        public StaticMessageRepository(AppDbContext context, ILogger<StaticMessageRepository> logger)
            : base(context, logger) { }

        public async Task<string> GetMessageByTypeAsync(BotPage page)
        {
            try
            {
                var message = await _context.StaticMessages
                .Where(m => m.PageId == (int)page)
                .Select(m => m.Message)
                .FirstOrDefaultAsync();

                return message;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on get message by type {typeof(StaticMessageRepository)}");
                throw;
            }
        }

        public async Task<IEnumerable<StaticMessageEntity>> GetAllAsync()
        {
            try
            {
                var messages = await _context.StaticMessages.ToListAsync();

                _logger.LogInformation($"Get all messages {typeof(StaticMessageRepository)}");

                return messages;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on get all messages {typeof(StaticMessageRepository)}");
                throw;
            }
        }

        public async Task<StaticMessageEntity> GetByGuidAsync(Guid messageGuid)
        {
            try
            {
                var message = await _context.StaticMessages
                    .FirstOrDefaultAsync(m => m.StaticMessageGuid == messageGuid);

                _logger.LogInformation($"Loading message with guid {message.StaticMessageGuid}");

                return message;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on get message {typeof(StaticMessageRepository)} with guid - {messageGuid}");
                throw;
            }
        }

        public async Task UpdateMessageAsync(StaticMessageEntity message)
        {
            try
            {
                await Update(message);

                _logger.LogInformation($"Updated message with guid {message.StaticMessageGuid}");
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on deleting message {typeof(StaticMessageRepository)} with guid - {message.StaticMessageGuid}");
                throw;
            }
        }
    }
}
