using Microsoft.EntityFrameworkCore;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class StaticMessageRepository : BaseRepository<StaticMessageEntity>
    {
        public StaticMessageRepository(AppDbContext context) : base(context) { }

        public async Task<string> GetMessageByTypeAsync(BotPage page)
        {
            var message = await _context.StaticMessages
                .Where(m => m.Page == page)
                .Select(m => m.Message)
                .FirstOrDefaultAsync();

            return message;
        }
    }
}
