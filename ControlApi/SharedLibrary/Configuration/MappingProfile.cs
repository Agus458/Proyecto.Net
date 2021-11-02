using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Buildings;
using SharedLibrary.DataTypes.Tenants;
using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects.
            CreateMap<User, UserDataType>();
            CreateMap<CreateUserRequestDataType, User>().ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null));

            CreateMap<Tenant, TenantDataType>();
            CreateMap<CreateTenantRequestDataType, Tenant>().ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null));
            CreateMap<UpdateTenantRequestDataType, Tenant>().ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null));

            CreateMap<Building, BuildingDataType>();
            CreateMap<CreateBuildingRequestDataType, Building>().ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null));
            CreateMap<UpdateBuildingRequestDataType, Building>().ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null));
        }
    }
}
