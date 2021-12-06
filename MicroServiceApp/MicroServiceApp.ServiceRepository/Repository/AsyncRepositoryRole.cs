using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryRole : IAsyncRepositoryRole<Role>
    {
        private protected readonly ContextDb _context;

        public AsyncRepositoryRole(ContextDb db)
        {
            _context = db;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
