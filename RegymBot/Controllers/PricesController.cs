using Microsoft.AspNetCore.Mvc;
using RegymBot.Data.Entities;
using RegymBot.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly PriceRepository _priceRepository;

        public PricesController(PriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var prices = await _priceRepository.GetAllAsync();

            return Ok(prices);
        }

        [HttpDelete]
        [Route("delete-price/{guid}")]
        public async Task<IActionResult> DeletePrice(Guid guid)
        {
            await _priceRepository.RemovePriceAsync(guid);

            return Ok();
        }

        [HttpPost]
        [Route("new-price")]
        public async Task<IActionResult> AddPrice(PriceEntity newPrice)
        {
            await _priceRepository.AddPriceAsync(newPrice);

            return Ok();
        }

        [HttpPost]
        [Route("update-price")]
        public async Task<IActionResult> UpdatePrice(PriceEntity price)
        {
            await _priceRepository.UpdatePriceAsync(price);

            return Ok();
        }
    }
}
