using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Portero")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService Service;

        public NotificationsController(INotificationService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take)
        {
            return Ok(await this.Service.GetAll(Skip, Take));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await this.Service.Delete(Id);
            return NoContent();
        }

        [HttpDelete("Clear")]
        public async Task<IActionResult> Clear()
        {
            await this.Service.Clear();
            return NoContent();
        }
    }
}
