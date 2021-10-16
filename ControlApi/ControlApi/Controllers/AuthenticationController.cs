using BusinessLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;
using SharedLibrary.DataTypes.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService Service;

        public AuthenticationController(IAuthenticationService Service)
        {
            this.Service = Service;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<dynamic>> Login(LoginRequestDataType Data)
        {
            var Result = await this.Service.Login(Data);

            if (Result != null && Result is not ApiError) return Ok(Result);

            return BadRequest(Result);
        }
    }
}
