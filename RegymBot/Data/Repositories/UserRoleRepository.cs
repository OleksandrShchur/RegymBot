using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using System;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRoleEntity>
    {
        private const string COACH_ROLE = "Тренер";
        public UserRoleRepository(AppDbContext context, ILogger<UserRoleRepository> logger)
            : base(context, logger) { }

        public async Task<RoleEntity> GetCoachRoleAsync()
        {
            try
            {
                var role = await _context.UserRoles
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(r => r.Role.Role == COACH_ROLE);

                return role.Role;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on get coach role {typeof(UserRoleRepository)}");
                throw;
            }
        }
    }
}
