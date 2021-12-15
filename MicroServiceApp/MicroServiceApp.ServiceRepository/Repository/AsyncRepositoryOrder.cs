using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryOrder :
        AsyncBaseRepository<Order>,
        IAsyncRepositoryOrder<Order>
    {
        public AsyncRepositoryOrder(ContextDb db) : base(db)
        {


        }
        public override async Task<IEnumerable<Order>> Get()
        {
            return await _context.Orders
                .Include(i => i.Car.ClientCar.User)
                .ToListAsync();
        }
    }
}






