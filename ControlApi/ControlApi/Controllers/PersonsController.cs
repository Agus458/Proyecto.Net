using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService Service;

        public PersonsController(IPersonsService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public IEnumerable<PersonDataType> GetAll()
        {
            return this.Service.GetAll();
        }

        [HttpGet("{Id}")]
        public PersonDataType GetById(Guid Id)
        {
            return this.Service.GetById(Id);
        }

        [HttpPost]
        public void Create(CreatePersonDataType Data)
        {
            this.Service.Create(Data);
        }
    }
}
