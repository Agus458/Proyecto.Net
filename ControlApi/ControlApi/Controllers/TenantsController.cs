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
        public IEnumerable<TenantDataType> GetAll()
        {
            return this.Service.GetAll();
        }

        [HttpGet("{Id}")]
        public TenantDataType GetById(Guid Id)
        {
            return this.Service.GetById(Id);
        }

        [HttpDelete("{Id}")]
        public async Task Delete(Guid Id)
        {
            await this.Service.Delete(Id);
        }

        [HttpPut("{Id}")]
        public void Update(Guid Id, UpdateTenantRequestDataType Data)
        {
            this.Service.Update(Id, Data);
        }

        [HttpPost]
        public async Task Create(CreateTenantRequestDataType Data)
        {
            await this.Service.Create(Data);
        }
    }
}
