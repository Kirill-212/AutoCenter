using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientCarController : ControllerBase
    {
        private readonly IAsyncRepositoryClientCar<ClientCar> asyncRepositoryClientCar;

        public ClientCarController(
            IAsyncRepositoryClientCar<ClientCar> asyncRepositoryClientCar
            )
        {
            this.asyncRepositoryClientCar = asyncRepositoryClientCar;
        }

        [HttpGet("GetByRegisterNumber")]
        public async Task<ClientCar> GetByRegisterNumber([FromQuery] string registerNumber)
        {
            return await asyncRepositoryClientCar.GetByRegisterNumber(registerNumber);
        }

        [HttpGet]
        public async Task<IEnumerable<ClientCar>> Get()
        {
            return await asyncRepositoryClientCar.Get();
        }

        [HttpGet("{id}")]
        public async Task<ClientCar> GetbyId(int id)
        {
            return await asyncRepositoryClientCar.FindById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] ClientCar item)
        {
            await asyncRepositoryClientCar.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] ClientCar item)
        {
            await asyncRepositoryClientCar.Update(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryClientCar.Remove(id);
        }
    }
}
