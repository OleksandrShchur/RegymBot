using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using RegymBot.Data.Repositories;
using RegymBot.Services;
using RegymBot.Services.Impl;
using System;
using System.Threading.Tasks;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PricesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly PriceRepository _priceRepository;

        public PricesController(
            IImageService imageService,
            PriceRepository priceRepository
            )
        {
            _imageService = imageService;
            _priceRepository = priceRepository;
        }


        [HttpPost]
        [Route("upload-image")]
        public async Task<IActionResult> UploadImage(RegymClub club)
        {
            var file = Request.Form.Files[0];
            await _imageService.UploadImageAsync(file, $"{club.ToString().ToLower()}-prices");

            return Ok();
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
