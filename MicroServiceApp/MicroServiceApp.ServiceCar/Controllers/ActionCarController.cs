﻿using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionCarController : Controller
    {
        private readonly IAsyncServiceActionCar<ActionCar> asyncServiceActionCar;

        public ActionCarController(IAsyncServiceActionCar<ActionCar> asyncServiceActionCar)
        {
            this.asyncServiceActionCar = asyncServiceActionCar;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
          return  StatusCode(await asyncServiceActionCar.DeleteAll());
        }
    }
}
