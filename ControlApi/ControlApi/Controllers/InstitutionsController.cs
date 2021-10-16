using BusinessLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly IInstitutionsService Service;

        public InstitutionsController(IInstitutionsService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public IEnumerable<InstitutionDataType> GetAll()
        {
            return this.Service.GetAll();
        }

        [HttpGet("{Id}")]
        public InstitutionDataType GetById(Guid Id)
        {
            return this.Service.GetById(Id);
        }
    }
}
