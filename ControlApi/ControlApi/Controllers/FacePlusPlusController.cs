using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedLibrary.Configuration.FacePlusPlus;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ControlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacePlusPlusController : ControllerBase
    {
        private readonly FacePlusPlusConfiguration Configuration;
        private readonly HttpClient HttpClient;

        public FacePlusPlusController(IOptions<FacePlusPlusConfiguration> Configuration, IHttpClientFactory HttpClientFactory)
        {
            this.Configuration = Configuration.Value;
            this.HttpClient = HttpClientFactory.CreateClient();
        }

        [HttpGet("GetDetail")]
        public async Task<dynamic> GetDetail()
        {
            return await FacePlusPlus.GetDetail(this.HttpClient, this.Configuration);
        }

        [HttpPost("CreateFaceSet")]
        public async Task<dynamic> CreateFaceSet()
        {
            return await FacePlusPlus.CreateFaceSet(this.HttpClient, this.Configuration);
        }

        [HttpDelete("DeleteFaceSet")]
        public async Task<dynamic> DeleteFaceSet()
        {
            return await FacePlusPlus.DeleteFaceSet(this.HttpClient, this.Configuration);
        }

        [HttpPost("Detect")]
        public async Task<IActionResult> Detect(IFormFile File)
        {
            try
            {
                if (File == null || File.ContentType != "image/jpeg") throw new ApiError("Archivo invalido");

                return Ok(await FacePlusPlus.Detect(this.HttpClient, this.Configuration, File.OpenReadStream(), File.FileName));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
