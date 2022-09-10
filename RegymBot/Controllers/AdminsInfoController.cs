using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegymBot.Data.Entities;
using System.Threading.Tasks;
using RegymBot.Data;
using Microsoft.EntityFrameworkCore;

namespace RegymBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminsInfoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AdminsInfoController(
            AppDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var adminsInfo = await _dbContext.AdminsInfo.AsNoTracking().FirstOrDefaultAsync(i => i.AdminsInfoId == 1);
            return Ok(adminsInfo);
        }

        [HttpGet("registration-links")]
        public async Task<IActionResult> GetRegistrationLinks()
        {
            var adminsInfo = await _dbContext.AdminsRegistrationLinks.AsNoTracking().FirstOrDefaultAsync(i => i.AdminsRegistrationLinksId == 1);
            return Ok(adminsInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AdminsInfo adminsInfo)
        {
            adminsInfo.AdminsInfoId = 1;
            _dbContext.AdminsInfo.Update(adminsInfo);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
