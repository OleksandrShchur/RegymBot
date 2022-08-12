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

        public async Task<PriceEntity> GetByGuidAsync(Guid priceGuid)
        {
            try
            {
                var price = await _context.Prices.FirstOrDefaultAsync(p => p.PriceGuid == priceGuid);

                _logger.LogInformation($"Got price by priceGuid - {price.PriceGuid}, {typeof(PriceEntity)}");

                return price;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on get price by guid {typeof(PriceEntity)}");
                throw;
            }
        }

        public async Task AddPriceAsync(PriceEntity newPrice)
        {
            try
            {
                var price = await Insert(newPrice);

                _logger.LogInformation($"Inserted new price {typeof(PriceEntity)} with guid - {price.PriceGuid}");
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on adding price {typeof(PriceEntity)}");
                throw;
            }
        }

        public async Task UpdatePriceAsync(PriceEntity price)
        {
            try
            {
                await Update(price);

                _logger.LogInformation($"Updated price {typeof(PriceEntity)} with guid - {price.PriceGuid}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on updating price {typeof(PriceEntity)}");
                throw;
            }
        }

        public async Task RemovePriceAsync(Guid priceGuid)
        {
            try
            {
                var price = await GetByGuidAsync(priceGuid);
                await Delete(price);

                _logger.LogInformation($"Deleted price {typeof(PriceEntity)} with guid - {price.PriceGuid}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on deleting price {typeof(PriceEntity)}");
                throw;
            }
        }
    }
}
