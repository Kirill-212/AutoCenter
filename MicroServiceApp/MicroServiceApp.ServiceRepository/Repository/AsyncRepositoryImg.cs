using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryImg :
        AsyncBaseRepository<Img>,
        IAsyncRepositoryImg<Img>
    {
        public AsyncRepositoryImg(ContextDb db) : base(db)
        {
        }

        public async Task AddRange(List<Img> items)
        {
            await _context.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Img> items)
        {
            _context.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
