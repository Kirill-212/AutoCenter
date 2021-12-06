using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAsyncRepositoryUser<User> asyncRepositoryUser;

        public UserController(IAsyncRepositoryUser<User> asyncRepositoryUser)
        {
            this.asyncRepositoryUser = asyncRepositoryUser;
        }

        [HttpGet("GetAllUsersNotAddedToEmp")]
        public async Task<IEnumerable<User>> GetAllUsersNotAddedToEmp()
        {
            return await asyncRepositoryUser.GetAllUsersNotAddedToEmp();
        }

        [HttpGet("GetByEmail")]
        public async Task<User> GetByEmail([FromQuery]string email)
        {
            return await asyncRepositoryUser.GetByEmail(email);
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await asyncRepositoryUser.Get();
        }

        [HttpGet("{id}")]
        public async Task<User> GetbyId(int id)
        {
            return await asyncRepositoryUser.FindById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] User item)
        {
            await asyncRepositoryUser.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] User item)
        {
            await asyncRepositoryUser.Update(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryUser.Remove(id);
        }
    }
}

