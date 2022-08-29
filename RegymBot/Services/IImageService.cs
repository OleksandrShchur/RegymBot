using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RegymBot.Services
{
    public interface IImageService
    {
        Task UploadImageAsync(IFormFile image, int club);
    }
}
