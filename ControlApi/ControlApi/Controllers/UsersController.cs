using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedLibrary.Configuration.FacePlusPlus;
using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService Service;
        private readonly HttpClient HttpClient;
        private readonly FacePlusPlusConfiguration Configuration;

        public UsersController(IUsersService Service, IHttpClientFactory HttpClientFactory, IOptions<FacePlusPlusConfiguration> Configuration)
        {
            this.Service = Service;
            this.HttpClient = HttpClientFactory.CreateClient();
            this.Configuration = Configuration.Value;
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

        [HttpGet("compare")]
        public async Task<dynamic> Compare()
        {
            return await FacePlusPlus.GetFaceSets(this.HttpClient, this.Configuration);
        }
    }
}
