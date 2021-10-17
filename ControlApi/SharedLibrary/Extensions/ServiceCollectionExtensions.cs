using DataAccessLibrary.Stores;
using DataAccessLibrary.Stores.StoresImplementations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMultitenancy(this IServiceCollection Services)
        {
            Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Services.AddScoped<ITenantsStore, TenantsStore>();

            return Services;
        }
    }
}
