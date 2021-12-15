using AutoMapper;
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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAsyncServiceUser<User> asyncService;

        public UserController(
            IAsyncServiceUser<User> asyncService,
            IMapper mapper
            )
        {
            _mapper = mapper;
            this.asyncService = asyncService;
        }

        [HttpGet("GetAllUsersNotAddedToEmp")]
        public async Task<IEnumerable<User>> GetAllUsersNotAddedToEmp()
        {
            return await asyncService.GetAllUsersNotAddedToEmp();
        }

        [HttpGet("GetUserByEmail")]
        public async Task<User> GetUserByEmail([FromQuery] string email)
        {
            return await asyncService.GetByEmail(email);
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await asyncService.GetAll(Request.Headers["Authorization"]);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<User> GetbyId(int id)
        {
            return await asyncService.FindById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostUserDto item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncService.Create(_mapper.Map<User>(item)));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PutUserDto item)
        {
            if (ModelState.IsValid)
            {

                return StatusCode(await asyncService.Update(_mapper.Map<User>(item)));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("UpdateStatus")]
        public async Task<ActionResult> PutStatus([FromQuery] string email)
        {
            return StatusCode(await asyncService.UpdateStatusByEmail(email));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string email)
        {
            return StatusCode(await asyncService.Remove(email));
        }
    }
}
