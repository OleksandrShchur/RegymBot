﻿using Microsoft.EntityFrameworkCore;
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
        private const string COACH_ROLE = "Тренер";

        public UserRepository(AppDbContext context,
            ILogger<UserRepository> logger,
            UserRoleRepository userRoleRepository)
            : base(context, logger) 
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IQueryable<UserEntity>> LoadCoachesQuery()
        {
            var coachRole = await _userRoleRepository.GetCoachRoleAsync();
            return _context.Users
                .Where(u => u.UserRoles.Any(ur => ur.RoleGuid == coachRole.RoleGuid));
        }

        public async Task<IEnumerable<UserEntity>> LoadAllAsync()
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .Include(u => u.UserClubs)
                        .ThenInclude(ur => ur.Club)
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

        public async Task<UserEntity> GetByGuidAsync(Guid userGuid)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .Include(u => u.UserClubs)
                    .FirstOrDefaultAsync(u => u.UserGuid == userGuid);

                _logger.LogInformation($"Loading user with guid {user.UserGuid}");

                return user;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on loading user {typeof(UserEntity)} with guid {userGuid}");
                throw;
            }
        }

        public async Task RemoveUserAsync(Guid userGuid)
        {
            try
            {
                var userToDelete = await GetByGuidAsync(userGuid);

                await Delete(userToDelete);

                _logger.LogInformation($"Deleted user successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on deleting user {typeof(UserEntity)} with guid {userGuid}");
                throw;
            }
        }

        public async Task<UserEntity> AddUserAsync(UserEntity newUser)
        {
            try
            {
                var userFromDb = await Insert(newUser);

                await _userRoleRepository.AddUserToRoleAsync(COACH_ROLE, userFromDb.UserGuid);
                newUser.UserClubs.Select(uc => uc.UserRef = userFromDb.UserGuid);
                _context.UserClubs.AddRange(newUser.UserClubs);
                _logger.LogInformation($"Successful insert new user {typeof(UserEntity)} with guid {newUser.UserGuid}");

                return userFromDb;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on adding user {typeof(UserEntity)}");
                throw;
            }
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            try
            {
                _context.UserClubs.RemoveRange(
                    _context.UserClubs.AsNoTracking().Where(uc => uc.UserRef == user.UserGuid)
                );
                _context.UserClubs.AddRange(user.UserClubs);
                await Update(user);

                _logger.LogInformation($"Updated user {user.UserGuid}");
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on updating user {typeof(UserEntity)} with guid {user.UserGuid}");
                throw;
            }
        }

        public async Task<bool> UserExistsAsync(Guid userGuid)
        {
            try
            {
                return await _context.Users.AnyAsync(u => u.UserGuid == userGuid);
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on checking that user exists or not, message: {e.Message}");
                throw;
            }
        }
    }
}
