using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using System;
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
                .Where(m => m.Page == page)
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
    }
}
