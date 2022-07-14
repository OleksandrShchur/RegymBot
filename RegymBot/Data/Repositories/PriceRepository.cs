using Microsoft.EntityFrameworkCore;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class PriceRepository : BaseRepository<PriceEntity>
    {
        public PriceRepository(AppDbContext context) : base(context) { }

        public async Task<IList> GetAllAsync()
        {
            var prices = await _context.Prices.ToListAsync();

            return prices;
        }

        public async Task<IList> GetPricesByTypeAsync(PriceItem item)
        {
            var prices = await _context.Prices
                .Where(p => p.PriceType == item)
                .ToListAsync();

            return prices;
        }
    }
}
