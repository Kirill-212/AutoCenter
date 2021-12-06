using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryUser :
        AsyncBaseRepository<User>,
        IAsyncRepositoryUser<User>
    {
        public AsyncRepositoryUser(ContextDb db) : base(db)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersNotAddedToEmp()
        {
            return await _context.Users.Include(i => i.Employee)
                .Where(r => r.Id != r.Employee.UserId)
                .ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users
                .Include(i=>i.Role)
                .Where(i => i.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
