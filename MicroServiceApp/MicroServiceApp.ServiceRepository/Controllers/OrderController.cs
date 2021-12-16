using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IAsyncRepositoryOrder<Order> asyncRepositoryOrder;

        public OrderController(IAsyncRepositoryOrder<Order> asyncRepositoryOrder)
        {
            this.asyncRepositoryOrder = asyncRepositoryOrder;
        }


        [HttpGet("{id}")]
        public async Task<Order> GetbyId(int id)
        {
            return await asyncRepositoryOrder.FindById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            return await asyncRepositoryOrder.Get();
        }

        [HttpPost]
        public async Task Post([FromBody] Order item)
        {
            await asyncRepositoryOrder.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] Order item)
        {
            await asyncRepositoryOrder.Update(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryOrder.Remove(id);
        }
    }
}
