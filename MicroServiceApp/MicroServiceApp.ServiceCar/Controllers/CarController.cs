using AutoMapper;
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
    public class CarController : Controller
    {
        private readonly IAsyncServiceCar<Car> asyncServiceCar;

        public CarController(
            IAsyncServiceCar<Car> asyncServiceCar
            )
        {

            this.asyncServiceCar = asyncServiceCar;
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> GetAll()
        {
            return await asyncServiceCar.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetbyId(int id)
        {
            Car return_car = await asyncServiceCar.GetById(id);

            return return_car == null ? BadRequest() : return_car;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostCarDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncServiceCar.Create(item));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PutCarDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncServiceCar.Update(item));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("DeleteByVin")]
        public async Task<ActionResult> Delete([FromQuery] string vin)
        {
            return StatusCode(await asyncServiceCar.Remove(vin));
        }
    }
}
