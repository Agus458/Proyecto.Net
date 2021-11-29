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
            return CreatedAtAction(nameof(GetById), new { Id = Result.Id }, Result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            await this.Service.Delete(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(string Id, UpdateUserRequestDataType Data)
        {
            await this.Service.Update(Id, Data);
            return NoContent();
        }
    }
}
