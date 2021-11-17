﻿using BusinessLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTypes.Novelties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsService Service;

        public RoomsController(IRoomsService Service)
        {
            this.Service = Service;
        }

        [HttpGet("Building/{BuildingId}")]
        public IActionResult GetAll([FromQuery(Name = "skip")] int Skip, [FromQuery(Name = "take")] int Take, Guid BuildingId)
        {
            return Ok(this.Service.GetAll(Skip, Take, BuildingId));
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
        public IActionResult Update(Guid Id, UpdateRoomRequestDataType Data)
        {
            this.Service.Update(Id, Data);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create(CreateRoomRequestDataType Data)
        {
            var result = this.Service.Create(Data);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }
    }
}
