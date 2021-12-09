using DataAccessLibrary.Entities;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetTenant(this HttpContext HttpContext)
        {
            if (HttpContext.Items.TryGetValue(ApiConstants.HttpContextTenant, out var Tenant))
            {
                if (Guid.TryParse(Tenant as string, out var Id))
                {
                    return Id;
                }
            }

            return Guid.Empty;
        }
    }
}
