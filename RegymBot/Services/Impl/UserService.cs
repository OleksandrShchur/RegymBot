using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RegymBot.Data.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RegymBot.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        private const string DIRECTORY = "avatars";

        public UserService(UserRepository userRepository, IWebHostEnvironment appEnvironment)
        {
            _userRepository = userRepository;
            _appEnvironment = appEnvironment;
        }

        public async Task UploadUserImageAsync(IFormFile file, Guid userGuid)
        {
            try
            {
                var userExists = await _userRepository.UserExistsAsync(userGuid);

                if (userExists)
                {
                    var filePath = $"{_appEnvironment.WebRootPath}\\{DIRECTORY}\\{userGuid}.jpg";

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
