﻿using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceNew.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : Controller
    {
        private readonly IAsyncServiceNew<New> asyncService;

        public NewController(IAsyncServiceNew<New> asyncService)
        {
            this.asyncService = asyncService;
        }

        [HttpGet]
        public async Task<IEnumerable<New>> GetAll()
        {
            return await asyncService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<New>> GetbyId(int id)
        {
            New return_new = await asyncService.FindById(id);
            return return_new == null ? BadRequest() : return_new;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewWrapperDto<PostNewDto> item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncService.Create(item));             
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] NewWrapperDto<PutNewDto> item)
        {
            if (ModelState.IsValid)
            {
                return StatusCode(await asyncService.Update(item));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery]string title)
        {
            return StatusCode(await asyncService.Remove(title));
        }
    }
}
