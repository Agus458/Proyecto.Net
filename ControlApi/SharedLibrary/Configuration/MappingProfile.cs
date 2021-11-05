using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Buildings;
using SharedLibrary.DataTypes.Doors;
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
            CreateMap<CreateUserRequestDataType, User>();

            CreateMap<Tenant, TenantDataType>();
            CreateMap<CreateTenantRequestDataType, Tenant>();
            CreateMap<UpdateTenantRequestDataType, Tenant>();

            CreateMap<Building, BuildingDataType>();
            CreateMap<CreateBuildingRequestDataType, Building>();
            CreateMap<UpdateBuildingRequestDataType, Building>();

            CreateMap<Door, DoorDataType>();
            CreateMap<CreateDoorRequestDataType, Door>();
            CreateMap<UpdateDoorRequestDataType, Door>();
        }
    }
}
