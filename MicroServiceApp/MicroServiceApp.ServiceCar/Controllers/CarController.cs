using AutoMapper;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("UpdateStatus")]
        public async Task<ActionResult> UpdateStatus([FromQuery] string vin)
        {
            return StatusCode(await asyncServiceCar.UpdateStatus(vin));
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("GetCarByEmail")]
        public async Task<IEnumerable<Car>> GetCarByEmail([FromQuery] string email)
        {
            return await asyncServiceCar.GetCarByEmail(email);
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("GetByVinNotAddedEmp")]
        public async Task<Car> GetByVinNotAddedEmp([FromQuery]string vin)
        {
            return await asyncServiceCar.GetByVinNotAddedEmpValidAttr(vin);
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("GetCarForUser")]
        public async Task<IEnumerable<Car>> GetCarForUser()
        {
            return await asyncServiceCar.GetCarForUser( );
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet]
        public async Task<IEnumerable<Car>> GetAll()
        {
            return await asyncServiceCar.GetAll( );
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetbyId(int id)
        {
            Car return_car = await asyncServiceCar.GetById(id);

            return return_car == null ? BadRequest() : return_car;
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("GetWithoutClientCar")]
        public async Task<IEnumerable<Car>> GetWithoutClientCar()
        {
            return await asyncServiceCar.GetWithoutClientCar( );
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("GetByVin")]
        public async Task<ActionResult<Car>> GetByVin([FromQuery] string vin)
        {
            Car return_car = await asyncServiceCar.GetByVin(vin);

            return return_car == null ? BadRequest() : return_car;
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
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

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
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

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpDelete("DeleteByVin")]
        public async Task<ActionResult> Delete([FromQuery] string vin)
        {
            return StatusCode(await asyncServiceCar.Remove(vin));
        }
    }
}
