using AutoMapper;
using DataAccessLibrary.Stores;
using DataAccessLibrary.Stores.StoresImplementations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SharedLibrary.Configuration;
using SharedLibrary.Configuration.FacePlusPlus;
using SharedLibrary.Configuration.PayPal;
using SharedLibrary.Configuration.Tenancy;
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
            Services.AddTransient<ITenantResolutionStrategy, HeaderResolutionStrategy>();
            Services.AddTransient<ITenantsStore, TenantsStore>();

            return Services;
        }

        public static IServiceCollection AddApiConfiguration(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.Configure<PayPalApiConfiguration>(Configuration.GetSection("PayPal"));
            Services.Configure<FacePlusPlusConfiguration>(Configuration.GetSection("FacePlusPlus"));

            return Services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection Services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.ForAllMaps((obj, cnfg) => cnfg.ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null)));
            });

            IMapper mapper = mappingConfig.CreateMapper();
            Services.AddSingleton(mapper);

            return Services;
        }
    }
}
