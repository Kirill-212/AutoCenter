using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryCar :
        AsyncBaseRepository<Car>,
        IAsyncRepositoryCar<Car>
    {
        public AsyncRepositoryCar(ContextDb db) : base(db)
        {
        }

        public override async Task<IEnumerable<Car>> Get()
        {
            return await _context.Cars
                .Include(i => i.ActionCar)
                .Include(i => i.ClientCar.User)
                .ToListAsync();
        }

        public override async Task<Car> FindById(int id)
        {
            return await _context.Cars.Include(i => i.ActionCar)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Car> GetByVin(string vin)
        {
            return await _context.Cars.Where(i => i.VIN == vin)
                .Include(i => i.ActionCar)
                .Include(i => i.ClientCar.User)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateRange(List<Car> items)
        {
            items.ForEach(m => _context.Entry(m).State = EntityState.Modified);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetWithoutClientCar()
        {
            return await _context.Cars.Where(i => i.ClientCar == null).Include(i=>i.ClientCar)
               .Include(i => i.ActionCar)
               .ToListAsync();
        }

        public async Task<Car> GetByVinNotAddedEmp(string vin)
        {
            return await _context.Cars
                .Where(i => i.VIN == vin)
                .Include(i => i.ClientCar)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Car>> GetCarForUser()
        {
            return await _context.Cars.Include(i => i.ActionCar)
                .Where(u => u.ClientCar == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetCarByEmail(string email)
        {
            return await _context.Cars.Include(i => i.ActionCar).Include(i=>i.ClientCar.User)
                 .Where(u => u.ClientCar.User.Email==email)
                 .ToListAsync();
        }
    }
}
