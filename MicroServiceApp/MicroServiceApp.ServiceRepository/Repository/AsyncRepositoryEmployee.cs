using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryEmployee :
        AsyncBaseRepository<Employee>,
        IAsyncRepositoryEmployee<Employee>
    {
        public AsyncRepositoryEmployee(ContextDb db) : base(db)
        {
        }

        public async Task<Employee> FindByIdUser(int id)
        {
            return await _context.Employees
                .Where(i => i.UserId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> FindByUserEmail(string email)
        {
            return await _context.Employees
                .Where(u => u.User.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
