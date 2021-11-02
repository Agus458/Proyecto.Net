using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingsService Service;

        public BuildingsController(IBuildingsService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take)
        {
            return Ok(this.Service.GetAll(Skip, Take));
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(this.Service.GetById(Id));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            this.Service.Delete(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Update(Guid Id, UpdateBuildingRequestDataType Data)
        {
            this.Service.Update(Id, Data);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(CreateBuildingRequestDataType Data)
        {
            var result = this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }
    }
}
