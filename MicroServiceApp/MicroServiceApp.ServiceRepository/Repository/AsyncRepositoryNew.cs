using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryNew :
        AsyncBaseRepository<New>,
        IAsyncRepositoryNew<New>
    {
        public AsyncRepositoryNew(ContextDb db) : base(db)
        {
        }

        public async Task<New> GetByTitle(string title)
        {
            return await _context.News.Include(i => i.Imgs)
                .Where(i => i.Title == title)
                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<New>> Get()
        {
            return await _context.News.Include(i => i.Imgs).ToListAsync();
        }

        public override async Task<New> FindById(int id)
        {
            return await _context.News
                .Include(i => i.Imgs)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
