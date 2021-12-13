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

        [HttpGet("Room/{RoomId}")]
        public IActionResult GetAll(Guid RoomId)
        {
            return Ok(this.Service.GetAll(RoomId));
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id, Guid RoomId)
        {
            return Ok(this.Service.GetById(Id, RoomId));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id, Guid RoomId)
        {
            this.Service.Delete(Id, RoomId);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(CreateEventRequestDataType Data)
        {
            var result = this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }

        [HttpPut("Room/{RoomId}/{Id}")]
        public IActionResult Update(Guid Id, UpdateEventRequestDataType Data, Guid RoomId)
        {
            this.Service.Update(Id, Data, RoomId);
            return NoContent();
        }
    }
}
