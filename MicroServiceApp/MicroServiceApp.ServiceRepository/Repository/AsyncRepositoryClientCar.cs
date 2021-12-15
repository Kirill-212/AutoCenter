using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public override async Task<IEnumerable<ClientCar>> Get()
        {
            return await _context.ClientsCars
                .Include(i => i.Car.ActionCar)
                .Include(i => i.User)
                .ToListAsync();
        }
    }
}
