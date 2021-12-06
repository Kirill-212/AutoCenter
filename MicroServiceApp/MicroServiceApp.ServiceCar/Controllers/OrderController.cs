using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IAsyncServiceOrder<Order> asyncServiceOrder;

        public OrderController(IAsyncServiceOrder<Order> asyncServiceOrder)
        {
            this.asyncServiceOrder = asyncServiceOrder;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostOrderDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncServiceOrder.Create(item));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetbyId(int id)
        {
            Order return_order = await asyncServiceOrder.GetById(id);
            if (return_order == null)
            {
                return BadRequest();
            }

            return return_order;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await asyncServiceOrder.GetAll();
        }
    }
}
