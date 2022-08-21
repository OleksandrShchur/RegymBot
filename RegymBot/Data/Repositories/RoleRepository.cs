using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using System;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class RoleRepository : BaseRepository<RoleEntity>
    {
        public RoleRepository(AppDbContext context,
            ILogger<RoleRepository> logger)
            : base(context, logger) { }

        public async Task<RoleEntity> GetRoleByNameAsync(string roleName)
        {
            try
            {
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Role == roleName);

                return role;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on get role by name {typeof(RoleEntity)}");
                throw;
            }
        }
    }
}
