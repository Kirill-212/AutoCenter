using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IAsyncRepositoryRole<Role> asyncRepositoryUser;

        public RoleController(IAsyncRepositoryRole<Role> asyncRepositoryUser)
        {
            this.asyncRepositoryUser = asyncRepositoryUser;
        }


        [HttpGet]
        public async Task<IEnumerable<Role>> GetAllUsersNotAddedToEmp()
        {
            return await asyncRepositoryUser.GetAll();
        }
    }
}
