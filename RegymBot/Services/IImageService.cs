using Microsoft.AspNetCore.Http;
using RegymBot.Data.Enums;
using System.Threading.Tasks;

namespace RegymBot.Services
{
    public interface IImageService
    {
        Task UploadImageAsync(IFormFile image, RegymClub club);
    }
}
