using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.Error
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate Next;

        public ApiExceptionMiddleware(RequestDelegate Next)
        {
            this.Next = Next;
        }

        public async Task InvokeAsync(HttpContext HttpContext)
        {
            try
            {
                await Next(HttpContext);
            }
            catch (Exception Exeption)
            {
                await HandleExceptionAsync(HttpContext, Exeption);
            }
        }

        private async Task HandleExceptionAsync(HttpContext Context, Exception Exeption)
        {
            Context.Response.ContentType = "application/json";

            object Message;

            if (Exeption is ApiError)
            {
                var Aux = Exeption as ApiError;
                Context.Response.StatusCode = Aux.StatusCode;
                Message = new
                {
                    Aux.StatusCode,
                    Exeption.Message
                };
            }
            else
            {
                Console.WriteLine(Exeption);
                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Message = new
                {
                    Context.Response.StatusCode,
                    Message = "Internal Server Error"
                };
            }

            await Context.Response.WriteAsync(JsonConvert.SerializeObject(Message));
        }
    }
}
