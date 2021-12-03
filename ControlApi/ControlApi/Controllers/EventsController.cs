using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService Service;

        public EventsController(IEventsService Service)
        {
            this.Service = Service;
        }

        [HttpGet("Building/{BuildingId}")]
        public IActionResult GetAll(Guid BuildingId)
        {
            return Ok(this.Service.GetAll(BuildingId));
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id, Guid BuildingId)
        {
            return Ok(this.Service.GetById(Id, BuildingId));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id, Guid BuildingId)
        {
            this.Service.Delete(Id, BuildingId);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(CreateEventRequestDataType Data)
        {
            var result = this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }

        [HttpPut("Building/{BuildingId}/{Id}")]
        public IActionResult Update(Guid Id, UpdateEventRequestDataType Data, Guid BuildingId)
        {
            this.Service.Update(Id, Data, BuildingId);
            return NoContent();
        }
    }
}
