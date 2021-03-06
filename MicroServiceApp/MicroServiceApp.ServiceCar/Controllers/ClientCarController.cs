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
    public class ClientCarController : Controller
    {
        private readonly IAsyncServiceClientCar<ClientCar> asyncServiceClientCar;

        public ClientCarController(IAsyncServiceClientCar<ClientCar> asyncServiceClientCar)
        {
            this.asyncServiceClientCar = asyncServiceClientCar;
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostClientCarDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncServiceClientCar.Create(item));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PutClientCarDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncServiceClientCar.Update(item));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpGet]
        public async Task<IEnumerable<ClientCar>> GetAll()
        {
            return await asyncServiceClientCar.GetAll( );
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientCar>> GetbyId(int id)
        {
            ClientCar return_clientCar = await asyncServiceClientCar.GetById(id);
            if (return_clientCar == null)
            {
                return BadRequest();
            }

            return return_clientCar;
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpDelete]
        public async Task<ActionResult> DeleteByRegisterNumber([FromQuery] string registerNumber)
        {
            return StatusCode(await asyncServiceClientCar.Remove(registerNumber));
        }
    }
}
