using BusinessLibrary.Services;
using ExcelDataReader;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedLibrary.Configuration.FacePlusPlus;
using SharedLibrary.DataTypes.Persons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Portero, Gestor")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService Service;

        public PersonsController(IPersonsService Service)
        {
            this.Service = Service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take)
        {
            return Ok(this.Service.GetAll(Skip, Take));
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            return Ok(this.Service.GetById(Id));
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            this.Service.Delete(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public IActionResult Update(Guid Id, UpdatePersonRequestDataType Data)
        {
            this.Service.Update(Id, Data);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreatePersonRequestDataType Data)
        {
            var result = this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }

        [HttpPost("csv")]
        public IActionResult CSV()
        {
            var Persons = new List<CreatePersonRequestDataType>();
            try
            {
                var file = Request.Form.Files[0];

                using (var stream = file.OpenReadStream())
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var conf = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };

                        var dataSet = reader.AsDataSet(conf);

                        foreach (DataRow Row in dataSet.Tables[0].Rows)
                        {
                            Persons.Add(new CreatePersonRequestDataType()
                            {
                                Name = Row["Name"]?.ToString(),
                                LastName = Row["LastName"]?.ToString(),
                                Document = Row["Document"]?.ToString(),
                                DocumentType = Row["DocumentType"]?.ToString(),
                                Email = Row["Email"]?.ToString(),
                                Phone = Row["Phone"]?.ToString()
                            });
                        }
                    }
                }

                foreach (var item in Persons)
                {
                    this.Service.Create(item);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(Persons);
        }
    }
}
