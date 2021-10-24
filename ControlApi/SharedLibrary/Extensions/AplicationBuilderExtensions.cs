using Microsoft.AspNetCore.Builder;
using SharedLibrary.Configuration.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class AplicationBuilderExtensions
    {
        public static void ConfigureApiExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiExceptionMiddleware>();
        }
    }
}
