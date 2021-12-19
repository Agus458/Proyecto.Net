using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Novelties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class NoveltiesController : ControllerBase
    {
        private readonly INoveltyService Service;

        public NoveltiesController(INoveltyService Service)
        {
            this.Service = Service;
        }

        [HttpGet("Building/{BuildingId}")]
        public IActionResult GetAll([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take, Guid BuildingId)
        {
            return Ok(this.Service.GetAll(Skip, Take, BuildingId));
        }

        [HttpGet("Tenant/{TenantId}")]
        [AllowAnonymous]
        public IActionResult GetByTenant([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take, Guid TenantId)
        {
            return Ok(this.Service.GetByTenant(Skip, Take, TenantId));
        }

        [HttpGet("Building/{BuildingId}/{Id}")]
        public IActionResult GetById(Guid Id, Guid BuildingId)
        {
            return Ok(this.Service.GetById(Id, BuildingId));
        }

        [HttpDelete("Building/{BuildingId}/{Id}")]
        public IActionResult Delete(Guid Id, Guid BuildingId)
        {
            this.Service.Delete(Id, BuildingId);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Update(Guid Id, [FromForm] UpdateNoveltyRequestDataType Data)
        {
            this.Service.Update(Id, Data);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateNoveltyRequestDataType Data)
        {
            var result = this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { BuildingId = result.BuildingId, Id = result.Id }, result);
        }
    }
}
