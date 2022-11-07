using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RegymBot.Data.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RegymBot.Services.Impl
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public ImageService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task UploadImageAsync(IFormFile image, string name)
        {


            try
            {
                string filePath = _appEnvironment.WebRootPath + $"\\{name}.jpg";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                if (image.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
