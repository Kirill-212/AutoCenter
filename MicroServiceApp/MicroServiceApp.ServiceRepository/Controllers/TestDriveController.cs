using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDriveController : Controller
    {
        private readonly IAsyncRepositoryTestDrive<TestDrive> asyncRepositoryTestDrive;

        public TestDriveController(IAsyncRepositoryTestDrive<TestDrive> asyncRepositoryTestDrive)
        {
            this.asyncRepositoryTestDrive = asyncRepositoryTestDrive;
        }


        [HttpGet]
        public async Task<IEnumerable<TestDrive>> Get()
        {
            return await asyncRepositoryTestDrive.Get();
        }


        [HttpPost]
        public async Task Post([FromBody] TestDrive item)
        {
            await asyncRepositoryTestDrive.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] TestDrive item)
        {
            await asyncRepositoryTestDrive.Update(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryTestDrive.Remove(id);
        }

        [HttpPut("DeleteRange")]
        public async Task DeleteRange([FromBody] List<TestDrive> items)
        {
            await asyncRepositoryTestDrive.DeleteRange(items);
        }

        [HttpGet("GetByVin")]
        public async Task<IEnumerable<TestDrive>> GetByVin([FromQuery]string vin)
        {
          return  await asyncRepositoryTestDrive.GetByVin(vin);
        }

        [HttpPost("GetByAllData")]
        public async Task<TestDrive> GetByAll([FromBody] TestDrive item)
        {
            return await asyncRepositoryTestDrive.GetByAllData(item);
        }

        [HttpGet("GetByVinAttr")]
        public async Task<IEnumerable<TestDrive>> GetByVinAttr([FromQuery] string vin)
        {
            return await asyncRepositoryTestDrive.GetByVin(vin);
        }
    }
}
