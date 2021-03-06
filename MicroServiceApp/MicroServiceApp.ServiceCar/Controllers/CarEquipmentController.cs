using AutoMapper;
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
    public class CarEquipmentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAsyncServiceCarEquipmentForm<CarEquipmentForm> equipmentForm;
        private readonly IAsyncServiceCarEquipment<CarEquipment> asyncService;

        public CarEquipmentController(
            IMapper mapper,
            IAsyncServiceCarEquipmentForm<CarEquipmentForm> equipmentForm,
            IAsyncServiceCarEquipment<CarEquipment> asyncService
            )
        {
            this.asyncService = asyncService;
            this.mapper = mapper;
            this.equipmentForm = equipmentForm;
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("GetForm")]
        public async Task<ActionResult<CarEquipmentFormDto>> GetForm()
        {
            return Ok(mapper.Map<CarEquipmentFormDto>(await equipmentForm.Get()));
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpPut("PutForm")]
        public async Task<ActionResult> UpdateForm(
            [FromBody] CarEquipmentFormDto carEquipmentFormDto
            )
        {
            if (ModelState.IsValid)
            {
                if (!await equipmentForm
                    .Update(mapper.Map<CarEquipmentForm>(carEquipmentFormDto))
                    )
                {
                    return BadRequest("Check your new field");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarEquipmentDto>>> GetAll()
        {
            return Ok(mapper.Map<IEnumerable<CarEquipmentDto>>(await asyncService.GetAll()));
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("GetByName")]
        public async Task<ActionResult<CarEquipmentDto>> GetByName([FromQuery] string name)
        {
            return Ok(mapper.Map<CarEquipmentDto>(await asyncService.GetByName(name)));
        }

        [AllowAnonymous]
        [HttpGet("GetByNameValidAttr")]
        public async Task<ActionResult<CarEquipmentDto>> GetByNameValidAttr([FromQuery] string name)
        {
            return Ok(mapper.Map<CarEquipmentDto>(await asyncService.GetByName(name)));
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CarEquipmentDto>> GetById(string id)
        {
            return Ok(mapper.Map<CarEquipmentDto>(await asyncService.GetById(id)));
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PostCarEquipmentDto item)
        {
            if (ModelState.IsValid)
            {
                await asyncService.Create(mapper.Map<CarEquipment>(item));
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] PutCarEquipmentDto item)
        {
            if (ModelState.IsValid)
            {
                await asyncService.Update(mapper.Map<CarEquipment>(item));
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = " ADMIN, EMPLOYEE, USER")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string name)
        {
            if (await asyncService.Remove(name) == null)
            {
                return BadRequest("check your CarEquipment Name");
            }

            return Ok();
        }
    }
}
