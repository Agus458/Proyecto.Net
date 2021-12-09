using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantsService Service;

        public TenantsController(ITenantsService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take)
        {
            return Ok(this.Service.GetAll(Skip, Take));
        }

        [HttpGet("List")]
        public IActionResult Get()
        {
            return Ok(this.Service.Get());
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(this.Service.GetById(Id));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await this.Service.Delete(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Update(Guid Id, UpdateTenantRequestDataType Data)
        {
            this.Service.Update(Id, Data);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(CreateTenantRequestDataType Data)
        {
            var result = this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }
    }
}
