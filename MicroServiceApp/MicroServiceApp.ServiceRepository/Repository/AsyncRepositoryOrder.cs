using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.ContextDB;

namespace MicroServiceApp.ServiceRepository.Repository
{
    public class AsyncRepositoryOrder :
        AsyncBaseRepository<Order>,
        IAsyncRepositoryOrder<Order>
    {
        public AsyncRepositoryOrder(ContextDb db) : base(db)
        {
        }
    }
}
