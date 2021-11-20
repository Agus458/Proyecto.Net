using Microsoft.AspNetCore.Builder;
using SharedLibrary.Configuration.Error;
using SharedLibrary.Configuration.Tenancy.Middlewares;
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

        public static void UseTenancy(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantMiddleware>();

            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/api/Authentication/Login"),
                appBuilder =>
                {
                    appBuilder.UseMiddleware<VerifyTenantMiddleware>();
                }
            );
        }
    }
}
