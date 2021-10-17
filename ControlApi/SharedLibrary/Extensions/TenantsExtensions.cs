using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class TenantsExtensions
    {
        public static TenantDataType GetDataType(this Tenant Institution)
        {
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<Tenant, TenantDataType>());
            var Mapper = new Mapper(Config);

            return Mapper.Map<TenantDataType>(Institution);
        }

        public static void AssignDataType(this Tenant Institution, CreateTenantRequestDataType Data)
        {
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<CreateTenantRequestDataType, Tenant>());
            var Mapper = new Mapper(Config);

            Mapper.Map(Data,Institution);
        }
    }
}
