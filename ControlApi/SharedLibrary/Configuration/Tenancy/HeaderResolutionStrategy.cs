using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration.Tenancy
{
    public class HeaderResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly HttpContext HttpContext;

        public HeaderResolutionStrategy(IHttpContextAccessor HttpContextAccesor)
        {
            this.HttpContext = HttpContextAccesor.HttpContext;
        }

        public string GetTenantIdentifier()
        {
            if (this.HttpContext.Request.Headers.TryGetValue("TenantIdentifier", out var Identifier))
            {
                return Identifier;
            }

            return null;
        }
    }
}
