using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace RegymBot.Data.Base
{
    public class BaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly ILogger _logger;

        public BaseRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public DbSet<T> Entities { get => _context.Set<T>(); }

        public async Task<T> Insert(T entity)
        {
            if (entity == null)
            {
                _logger.LogWarning($"Entity {typeof(T).FullName} is null");
            }

            try
            {
                Entities.Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on insert {typeof(T).FullName}");
                throw;
            }
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                _logger.LogWarning($"Entity {typeof(T).FullName} is null");
            }

            try
            {
                Entities.Update(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on update {typeof(T).FullName}");
                throw;
            }
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                _logger.LogWarning($"Entity {typeof(T).FullName} is null");
            }

            try
            {
                Entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on delete {typeof(T).FullName}");
                throw;
            }
        }

    }
}
