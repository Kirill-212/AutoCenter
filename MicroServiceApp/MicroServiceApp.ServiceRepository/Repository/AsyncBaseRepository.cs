using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncBaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private protected readonly ContextDb _context;
        private protected readonly DbSet<T> _dbSet;

        public AsyncBaseRepository(ContextDb db)
        {
            _context = db;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Create(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _dbSet.Remove(await FindById(id));
            await _context.SaveChangesAsync();
        }
    }
}
