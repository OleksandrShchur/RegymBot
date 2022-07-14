using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace RegymBot.Data.Base
{
    public class BaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        protected DbSet<T> Entities { get => _context.Set<T>(); }

        public async Task<T> Insert(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            Entities.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            Entities.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
