using BusinessLibrary.Services;
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
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantsService Service;

        public TenantsController(ITenantsService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public IEnumerable<TenantDataType> GetAll()
        {
            return this.Service.GetAll();
        }

        [HttpGet("{Id}")]
        public TenantDataType GetById(Guid Id)
        {
            return this.Service.GetById(Id);
        }
    }
}
