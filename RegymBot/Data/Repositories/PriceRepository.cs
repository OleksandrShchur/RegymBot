using Microsoft.EntityFrameworkCore;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using System.Collections;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class PriceRepository : BaseRepository<PriceEntity>
    {
        public PriceRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable> GetAllAsync()
        {
            var prices = await _context.Prices.ToListAsync();

            return prices;
        }
    }
}
