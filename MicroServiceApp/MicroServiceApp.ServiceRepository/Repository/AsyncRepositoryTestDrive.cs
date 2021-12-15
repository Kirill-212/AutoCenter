using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryTestDrive : AsyncBaseRepository<TestDrive>,
        IAsyncRepositoryTestDrive<TestDrive>
    {
        public AsyncRepositoryTestDrive(ContextDb db) : base(db)
        {
        }

        public async Task DeleteRange(List<TestDrive> items)
        {
            _context.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<TestDrive> GetByAllData(TestDrive item)
        {
            return await _context.TestDrives
               .Include(u => u.Car)
               .Where(u => u.Time==item.Time)
               .Where(u => u.Date == item.Date)
               .Where(u => u.Car == item.Car)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TestDrive>> GetByVin(string vin)
        {
            return await _context.TestDrives
                .Include(u => u.Car)
                .Where(u => u.Car.ClientCar == null)
                .Where(u => u.Car.VIN == vin)
                .ToListAsync();
        }

        public override async Task<IEnumerable<TestDrive>> Get()
        {
            return await _context.TestDrives
                .Include(i => i.Car)     
                .ToListAsync();
        }
    }
}
