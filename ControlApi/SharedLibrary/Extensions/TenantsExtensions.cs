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

        public static void AssignDataType<SDataType>(this Tenant Tenant, SDataType Data)
        {
            var Config = new MapperConfiguration(Conf =>
            {
                Conf.CreateMap<SDataType, Tenant>().ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null));
            });
            var Mapper = new Mapper(Config);

            Mapper.Map(Data, Tenant);
        }
    }
}
