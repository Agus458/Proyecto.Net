using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService Service;

        public UsersController(IUsersService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(this.Service.GetAll());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            return Ok(await this.Service.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequestDataType Data)
        {
            var Result = await this.Service.Create(Data);
            if(Result != null) return CreatedAtAction(nameof(GetById), new { Id = Result.Id }, Result);
            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            await this.Service.Delete(Id);
            return NoContent();
        }
    }
}
