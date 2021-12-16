using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : ControllerBase
    {
        private readonly IAsyncRepositoryNew<New> asyncRepositoryNew;

        public NewController(IAsyncRepositoryNew<New> asyncRepositoryNew)
        {
            this.asyncRepositoryNew = asyncRepositoryNew;
        }


        [HttpGet]
        public async Task<IEnumerable<New>> Get()
        {
            return await asyncRepositoryNew.Get();
        }

        [HttpGet("GetByTitle")]
        public async Task<New> GetbyTitle([FromQuery] string title)
        {
            return await asyncRepositoryNew.GetByTitle(title);
        }

        [HttpGet("GetByTitleValidAttr")]
        public async Task<New> GetByTitleValidAttr([FromQuery] string title)
        {
            return await asyncRepositoryNew.GetByTitle(title);
        }

        [HttpGet("{id}")]
        public async Task<New> GetbyId(int id)
        {
            return await asyncRepositoryNew.FindById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] New item)
        {
            await asyncRepositoryNew.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] New item)
        {
            await asyncRepositoryNew.Update(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryNew.Remove(id);
        }
    }
}
