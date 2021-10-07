using BusinessLayer.BusinessControllersInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTypes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusinessController Controller;

        public UsersController(IUsersBusinessController Controller)
        {
            this.Controller = Controller;
        }

        [HttpGet]
        public IEnumerable<UserDataType> GetUsers()
        {
            return this.Controller.GetUsers();
        }

        [HttpGet("{Id}")]
        public ActionResult<UserDataType> GetUser(Guid Id)
        {
            var User = this.Controller.GetUser(Id);

            if (User == null) return NotFound();

            return User;
        }

        [HttpPost]
        public ActionResult<UserDataType> CreateUser(CreateUserDataType Data)
        {
            var User = this.Controller.CreateUser(Data);

            return CreatedAtAction(nameof(GetUser), new { Id = User.Id }, User);
        }
    }
}
