using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RegymBot.Data.Repositories;
using RegymBot.AccountService;
using PasswordHashing;
using RegymBot.Data.Models;

namespace Doct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CredentialsRepository credsRepository;
        private readonly ILogger<AuthController> logger;
        private readonly ITokenService tokenService;

        public AuthController(
            CredentialsRepository credsRepository,
            ILogger<AuthController> logger,
            ITokenService tokenService
        )
        {
            this.credsRepository = credsRepository;
            this.logger = logger;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(CredentialsModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }
            
            var creds = this.credsRepository.Entities.FirstOrDefault(c => c.Login == loginModel.Login);
            if (creds == null) { 
                return Forbid();
            }

            if (PasswordHasher.Validate(loginModel.Password, creds.Password))
            {
                CredentialsModel user = new CredentialsModel() 
                { 
                    Login = creds.Login,
                    Token = tokenService.Authenticate(creds.CredentialsGuid, creds.Login)
                };

                return Ok(user);
            }
            return Forbid();
        }

        
        [HttpGet("ping")]
        public async Task<ActionResult> Ping()
        {
            return Ok(DateTime.Now);
        }
    }
}