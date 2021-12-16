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
    public class TestDriveController : Controller
    {
        private readonly IAsyncServiceTestDrive<TestDrive> asyncServiceTestDrive;

        public TestDriveController(IAsyncServiceTestDrive<TestDrive> asyncServiceTestDrive)
        {
            this.asyncServiceTestDrive = asyncServiceTestDrive;
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpGet]
        public async Task<IEnumerable<TestDrive>> GetAll()
        {
            return await asyncServiceTestDrive.GetAll();
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostTestDriveDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncServiceTestDrive.Create(item));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [Authorize(Roles = "ADMIN, EMPLOYEE, USER")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PutTestDriveDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncServiceTestDrive.Put(item));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
