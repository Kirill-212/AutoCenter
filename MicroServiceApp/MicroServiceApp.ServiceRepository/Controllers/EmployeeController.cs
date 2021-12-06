using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IAsyncRepositoryEmployee<Employee> asyncRepositoryEmployee;

        public EmployeeController(
            IAsyncRepositoryEmployee<Employee> asyncRepositoryEmployee
            )
        {
            this.asyncRepositoryEmployee = asyncRepositoryEmployee;
        }

        [HttpGet("GetByUserEmail")]
        public async Task<Employee> GetByUserEmail([FromQuery]string email)
        {
            return await asyncRepositoryEmployee.FindByUserEmail(email);
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await asyncRepositoryEmployee.Get();
        }

        [HttpGet("GetByUserId")]
        public async Task<Employee> GetByUserId([FromQuery] int userId)
        {
            return await asyncRepositoryEmployee.FindByIdUser(userId);
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetbyId(int id)
        {
            return await asyncRepositoryEmployee.FindById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Employee item)
        {
            await asyncRepositoryEmployee.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] Employee item)
        {
            await asyncRepositoryEmployee.Update(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryEmployee.Remove(id);
        }
    }
}
