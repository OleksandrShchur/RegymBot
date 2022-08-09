using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RegymBot.Data.Entities;
using RegymBot.Data.Models;
using RegymBot.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(UserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.LoadAllAsync();

            return Ok(_mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserModel>>(users));
        }
    }
}
