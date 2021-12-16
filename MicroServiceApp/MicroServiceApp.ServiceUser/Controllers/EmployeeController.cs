using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceUser.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IAsyncServiceEmployee<Employee> asyncService;

        public EmployeeController(
            IAsyncServiceEmployee<Employee> asyncService
            )
        {
            this.asyncService = asyncService;
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("GetByUserId")]
        public async Task<Employee> GetbyUserId([FromQuery] string email)
        {
            return await asyncService.FindByUserEmail(email); 
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await asyncService.GetAll();
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("{id}")]
        public async Task<Employee> GetbyId(int id)
        {
            return await asyncService.FindById(id);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostEmployeeDto item)
        {
            if (ModelState.IsValid)
            {
               return StatusCode(await asyncService.Create(item));

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PutEmployeeDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncService.Update(item));
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpDelete]
        public async Task<ActionResult> Delete(string email)
        {
            return StatusCode(await asyncService.Remove(email));
        }
    }
}
