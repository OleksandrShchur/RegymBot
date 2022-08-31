using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly StaticMessageRepository _staticMessageRepository;
        private readonly IMapper _mapper;

        public MessagesController(StaticMessageRepository staticMessageRepository, IMapper mapper)
        {
            _staticMessageRepository = staticMessageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _staticMessageRepository.GetAllAsync();
            var mappedMessages = _mapper.Map<IEnumerable<StaticMessageEntity>, IEnumerable<MessageModel>>(messages);

            return Ok(mappedMessages);
        }

        [HttpPost]
        [Route("update-message")]
        public async Task<IActionResult> UpdateMessage(MessageModel message)
        {
            var mappedMessage = _mapper.Map<MessageModel, StaticMessageEntity>(message);
            await _staticMessageRepository.UpdateMessageAsync(mappedMessage, message.PageName);

            return Ok();
        }
    }
}
