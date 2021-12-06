using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceUser.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IAsyncServiceRole<Role> asyncService;

        public RoleController(
            IAsyncServiceRole<Role> asyncService
            )
        {
            this.asyncService = asyncService;
        }

        [HttpGet]
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await asyncService.GetAll();
        }
    }
}
