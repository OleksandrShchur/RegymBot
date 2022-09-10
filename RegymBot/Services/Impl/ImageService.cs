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

        public async Task UploadImageAsync(IFormFile image, RegymClub club)
        {
            string filePath;
            try
            {
                switch (club)
                {
                    case RegymClub.Apollo:
                        filePath = _appEnvironment.WebRootPath + "\\apollo.jpg";
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

                        break;

                    case RegymClub.Vavylon:
                        filePath = _appEnvironment.WebRootPath + "\\vavylon.jpg";
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

                        break;

                    case RegymClub.PSHKN:
                        filePath = _appEnvironment.WebRootPath + "\\pshkn.jpg";
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

                        break;
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
