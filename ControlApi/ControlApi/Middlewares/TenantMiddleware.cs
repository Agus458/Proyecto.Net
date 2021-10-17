using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Configuration.Tenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlApi.Middlewares
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
            if (!Context.Items.ContainsKey("Tenant"))
            {
                var ResolutionStrategy = Context.RequestServices.GetService(typeof(ITenantResolutionStrategy)) as ITenantResolutionStrategy;
                var TenantsStore = Context.RequestServices.GetService(typeof(ITenantsStore)) as ITenantsStore;

                var Identifier = ResolutionStrategy.GetTenantIdentifier();
                var Tenant = TenantsStore.GetBySocialReason(Identifier);

                Context.Items.Add("Tenant", Tenant);
            }

            if (next != null)
                await next(Context);
        }
    }
}
