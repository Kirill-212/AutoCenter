using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IAsyncRepositoryCar<Car> asyncRepositoryCar;

        public CarController(IAsyncRepositoryCar<Car> asyncRepositoryCar)
        {
            this.asyncRepositoryCar = asyncRepositoryCar;
        }

        [HttpGet("GetCarForUser")]
        public async Task<IEnumerable<Car>> GetCarForUser()
        {
            return await asyncRepositoryCar.GetCarForUser();
        }

        [HttpGet("GetCarByEmail")]
        public async Task<IEnumerable<Car>> GetCarByEmail([FromQuery] string email)
        {
            return await asyncRepositoryCar.GetCarByEmail(email);
        }

        [HttpGet("GetByVinNotAddedEmpValidAttr")]
        public async Task<Car> GetByVinNotAddedEmpValidAttr([FromQuery] string vin)
        {
            return await asyncRepositoryCar.GetByVinNotAddedEmp(vin);
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> Get()
        {
            return await asyncRepositoryCar.Get();
        }

        [HttpGet("GetWithoutClientCar")]
        public async Task<IEnumerable<Car>> GetWithoutClientCar()
        {
            return await asyncRepositoryCar.GetWithoutClientCar();
        }

        [HttpGet("GetByVin")]
        public async Task<Car> GetbyVin([FromQuery] string vin)
        {
            return await asyncRepositoryCar.GetByVin(vin);
        }

        [HttpGet("GetByVinValidAttr")]
        public async Task<Car> GetByVinValidAttr([FromQuery] string vin)
        {
            return await asyncRepositoryCar.GetByVin(vin);
        }

        [HttpGet("{id}")]
        public async Task<Car> GetbyId(int id)
        {
            return await asyncRepositoryCar.FindById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Car item)
        {
            await asyncRepositoryCar.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] Car item)
        {
            await asyncRepositoryCar.Update(item);
        }

        [HttpPut("UpdateRange")]
        public async Task UpdateRange([FromBody] List<Car> items)
        {
            await asyncRepositoryCar.UpdateRange(items);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryCar.Remove(id);
        }
    }
}
