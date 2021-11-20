using DataAccessLibrary.Entities;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Error;
using SharedLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.Tenancy.Middlewares
{
    public class VerifyTenantMiddleware
    {
        private readonly RequestDelegate next;

        public VerifyTenantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext Context)
        {
            var Tenant = Context.GetTenant();

            if (next != null && (Context.User.IsInRole("SuperAdmin") || (Tenant != Guid.Empty && Context.User.FindFirst(c => c.Type.Equals("Tenant"))?.Value == Tenant.ToString())))
            {
                await next(Context);
            }
            else
            {
                throw new ApiError("Unautorized", (int)HttpStatusCode.Unauthorized);
            }
        }
    }
}