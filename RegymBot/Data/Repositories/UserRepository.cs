using Microsoft.EntityFrameworkCore;
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

        public async Task<List<UserEntity>> LoadCoachesByCategoryAsync(Category? category)
        {
            try
            {
                var coachRole = await _userRoleRepository.GetCoachRoleAsync();
                var coaches = await _context.Users
                    .Where(u => u.UserRoles.Any(ur => ur.RoleGuid == coachRole.RoleGuid) && u.Category == category)
                    .ToListAsync();

                _logger.LogInformation("Loading the list of coaches by category");

                return coaches;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on get coachs by category {typeof(UserEntity)}");
                throw;
            }
        }

        public async Task<IEnumerable<UserEntity>> LoadAllAsync()
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .ToListAsync();
                _logger.LogInformation("Loading the list of all users");

                return users;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on get coachs by category {typeof(UserEntity)}");
                throw;
            }
        }
    }
}
