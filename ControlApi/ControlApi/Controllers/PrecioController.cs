using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Precio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
    public class PrecioController : ControllerBase
    {
        private readonly IPrecioService Service;


        public PrecioController(IPrecioService Service)
        {
            this.Service = Service;
        }

        [HttpGet("Product/{ProductId}")]
        public IActionResult GetAll([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take, Guid ProductId)
        {
            return Ok(this.Service.GetAll(Skip, Take, ProductId));
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(this.Service.GetBuId(Id));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            this.Service.Delete(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Update(Guid Id, UpdatePreciosRequestDataType Data)
        {
            this.Service.Update(Id, Data);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(CreatePrecioRequestDataType Data)
        {
            var result = this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }

    }
}
