using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryActionCar :
        AsyncBaseRepository<ActionCar>,
        IAsyncRepositoryActionCar<ActionCar>
    {
        public AsyncRepositoryActionCar(ContextDb db) : base(db)
        {
        }

        public async Task DeleteRange(List<ActionCar> items)
        {
            _context.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionCar> GetBySharePercentage(int sharePercentage)
        {
            return await _context.ActionsCars
                .Where(i => i.SharePercentage == sharePercentage)
                .FirstOrDefaultAsync();
        }
    }
}
