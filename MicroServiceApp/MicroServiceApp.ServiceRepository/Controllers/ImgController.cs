using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgController : ControllerBase
    {
        private readonly IAsyncRepositoryImg<Img> asyncRepositoryImg;

        public ImgController(IAsyncRepositoryImg<Img> asyncRepositoryImg)
        {
            this.asyncRepositoryImg = asyncRepositoryImg;
        }

        [HttpGet]
        public async Task<IEnumerable<Img>> Get()
        {
            return await asyncRepositoryImg.Get();
        }

        [HttpGet("{id}")]
        public async Task<Img> GetbyId(int id)
        {
            return await asyncRepositoryImg.FindById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Img item)
        {
            await asyncRepositoryImg.Create(item);
        }

        [HttpPut]
        public async Task Put([FromBody] Img item)
        {
            await asyncRepositoryImg.Update(item);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await asyncRepositoryImg.Remove(id);
        }

        [HttpPut("DeleteRange")]
        public async Task DeleteRange([FromBody] List<Img> items)
        {
            await asyncRepositoryImg.DeleteRange(items);
        }

        [HttpPost("PostRange")]
        public async Task PostRange([FromBody] List<Img> items)
        {
            await asyncRepositoryImg.AddRange(items);
        }
    }
}
