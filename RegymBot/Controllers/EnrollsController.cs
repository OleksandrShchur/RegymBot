using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegymBot.Data.Entities;
using RegymBot.Data.Models;
using RegymBot.Data.Repositories;
using System.Threading.Tasks;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollsController : ControllerBase
    {
        private readonly ClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public EnrollsController(
            ClientRepository clientRepository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enrolls = await _clientRepository.LoadAllAsync();

            return Ok(enrolls);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ClientModel enroll)
        {
            var mappedEnroll = _mapper.Map<ClientModel, ClientEntity>(enroll);
            await _clientRepository.UpdateEnrollAsync(mappedEnroll);

            return Ok();
        }
    }
}
