using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class PriceRepository : BaseRepository<PriceEntity>
    {
        public PriceRepository(AppDbContext context, ILogger<PriceRepository> logger) 
            : base(context, logger) { }

        public async Task<IList> GetAllAsync()
        {
            try
            {
                var prices = await _context.Prices.ToListAsync();

                return prices;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on get all {typeof(PriceEntity)}");
                throw;
            }
        }

        public async Task<IList> GetPricesByTypeAsync(PriceItem item)
        {
            try 
            { 
                var prices = await _context.Prices
                    .Where(p => p.PriceType == item)
                    .ToListAsync();

                return prices;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on get prices by type {typeof(PriceEntity)}");
                throw;
            }
}
    }
}
