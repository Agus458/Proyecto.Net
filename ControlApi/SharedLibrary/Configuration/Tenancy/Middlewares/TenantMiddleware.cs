using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.Tenancy.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate next;

        public TenantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext Context)
        {
            var ResolutionStrategy = Context.RequestServices.GetService(typeof(ITenantResolutionStrategy)) as ITenantResolutionStrategy;
            var Identifier = ResolutionStrategy.GetTenantIdentifier();

            var TenantsStore = Context.RequestServices.GetService(typeof(ITenantsStore)) as ITenantsStore;
            var Tenant = TenantsStore.GetBySocialReason(Identifier);

            if (Tenant != null) {
                Context.Items.Add(ApiConstants.HttpContextTenant, Tenant);
            } else
            {
                Context.Items.Remove(ApiConstants.HttpContextTenant);
            }

            if (next != null)
                await next(Context);
        }
    }
}
