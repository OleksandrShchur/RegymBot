using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using RegymBot.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>
    {
        private readonly UserRoleRepository _userRoleRepository;
        public UserRepository(AppDbContext context,
            ILogger<UserRepository> logger,
            UserRoleRepository userRoleRepository)
            : base(context, logger) 
        {
            _userRoleRepository = userRoleRepository;
        }

        public IEnumerable<UserEntity> LoadCoachesByCategory(Category category)
        {
            try
            {
                var coachRole = _userRoleRepository.GetCoachRole();
                var coaches = _context.Users
                    .Where(u => u.UserRoles.Any(ur => ur.RoleGuid == coachRole.RoleGuid) && u.Category == category)
                    .ToList();

                return coaches;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on get coachs by category {typeof(UserEntity)}");
                throw;
            }
        }
    }
}
