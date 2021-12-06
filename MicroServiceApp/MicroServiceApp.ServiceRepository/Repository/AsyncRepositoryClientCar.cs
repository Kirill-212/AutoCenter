using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryClientCar :
        AsyncBaseRepository<ClientCar>,
        IAsyncRepositoryClientCar<ClientCar>
    {
        public AsyncRepositoryClientCar(ContextDb db) : base(db)
        {
        }

        public async Task<ClientCar> GetByRegisterNumber(string registerNumber)
        {
            return await _context.ClientsCars
                .Where(i => i.RegisterNumber == registerNumber)
                .FirstOrDefaultAsync();
        }
    }
}
