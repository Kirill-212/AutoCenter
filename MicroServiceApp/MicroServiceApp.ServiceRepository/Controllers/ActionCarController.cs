using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionCarController : ControllerBase
    {
        private readonly IAsyncRepositoryActionCar<ActionCar> asyncRepositoryActionCar;

        public ActionCarController(
            IAsyncRepositoryActionCar<ActionCar> asyncRepositoryActionCar
            )
        {
            this.asyncRepositoryActionCar = asyncRepositoryActionCar;
        }

        [HttpGet]
        public async Task<IEnumerable<ActionCar>> Get()
        {
            return await asyncRepositoryActionCar.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionCar> GetbyId(int id)
        {
            return await asyncRepositoryActionCar.FindById(id);
        }

        [HttpGet("GetBySharePercentage")]
        public async Task<ActionCar> Getbyv(int sharePercentage)
        {
            return await asyncRepositoryActionCar.GetBySharePercentage(sharePercentage);
        }

        [HttpPut("DeleteRange")]
        public async Task DeleteRange([FromBody] List<ActionCar> items)
        {
            await asyncRepositoryActionCar.DeleteRange(items);
        }

        [HttpPost]
        public async Task Post([FromBody] ActionCar item)
        {
            await asyncRepositoryActionCar.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] ActionCar item)
        {
            await asyncRepositoryActionCar.Update(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryActionCar.Remove(id);
        }
    }
}
