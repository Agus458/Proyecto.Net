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
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService Service;

        public UsersController(IUsersService Service)
        {
            this.Service = Service;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDataType>> GetAll()
        {
            return Ok(this.Service.GetAll());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
        [HttpGet("{Id}")]
        public ActionResult<UserDataType> GetById(string Id)
        {
            return Ok(this.Service.GetById(Id));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult<UserDataType> Create(CreateUserRequestDataType Data)
        {
            throw new NotImplementedException();
        }
    }
}
