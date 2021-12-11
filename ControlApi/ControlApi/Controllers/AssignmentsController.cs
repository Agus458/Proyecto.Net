using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Portero")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentsService Service;

        public AssignmentsController(IAssignmentsService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take)
        {
            return Ok(await this.Service.GetAll(Skip, Take));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            return Ok(await this.Service.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssignmentRequestDataType Data)
        {
            var result = await this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }

        [HttpGet("Doors")]
        public async Task<IActionResult> GetDoors([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take)
        {
            return Ok(await this.Service.GetDoors(Skip, Take));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await this.Service.Delete(Id);
            return NoContent();
        }
    }
}
