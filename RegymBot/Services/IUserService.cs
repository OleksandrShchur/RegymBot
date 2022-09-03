using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RegymBot.Services
{
    public interface IUserService
    {
        Task UploadUserImageAsync(IFormFile file, Guid userGuid);
    }
}
