using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegymBot.Data.DTOs;
using RegymBot.Data.Entities;
using RegymBot.Data.Models;
using RegymBot.Data.Repositories;
using RegymBot.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(UserRepository userRepository,
            IUserService userService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.LoadAllAsync();

            return Ok(_mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserModel>>(users));
        }

        [HttpDelete]
        [Route("delete-user/{guid}")]
        public async Task<IActionResult> DeleteUser(Guid guid)
        {
            await _userRepository.RemoveUserAsync(guid);

            return Ok();
        }

        [HttpPost]
        [Route("new-user")]
        public async Task<IActionResult> AddUser(UserModel user)
        {
            var mappedUser = _mapper.Map<UserModel, UserEntity>(user);
            var addedUser = await _userRepository.AddUserAsync(mappedUser);

            return Ok();
        }

        [HttpPost]
        [Route("update-user")]
        public async Task<IActionResult> UpdateUser(UserModel user)
        {
            var mappedUser = _mapper.Map<UserModel, UserEntity>(user);
            await _userRepository.UpdateUserAsync(mappedUser);

            return Ok();
        }

        [HttpPost]
        [Route("upload-avatar")]
        public async Task<IActionResult> UploadAvatar(Guid userGuid)
        {
            var file = Request.Form.Files[0];
            await _userService.UploadUserImageAsync(file, userGuid);

            return Ok();
        }
    }
}
