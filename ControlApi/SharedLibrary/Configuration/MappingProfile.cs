using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Assignment;
using SharedLibrary.DataTypes.Buildings;
using SharedLibrary.DataTypes.Doors;
using SharedLibrary.DataTypes.Entries;
using SharedLibrary.DataTypes.Events;
using SharedLibrary.DataTypes.Notifications;
using SharedLibrary.DataTypes.Novelties;
using SharedLibrary.DataTypes.Persons;
using SharedLibrary.DataTypes.Rooms;
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
            CreateMap<UpdateUserRequestDataType, User>();

            CreateMap<Tenant, TenantDataType>();
            CreateMap<CreateTenantRequestDataType, Tenant>();
            CreateMap<UpdateTenantRequestDataType, Tenant>();

            CreateMap<Building, BuildingDataType>();
            CreateMap<CreateBuildingRequestDataType, Building>();
            CreateMap<UpdateBuildingRequestDataType, Building>();

            CreateMap<Door, DoorDataType>();
            CreateMap<CreateDoorRequestDataType, Door>();
            CreateMap<UpdateDoorRequestDataType, Door>();

            CreateMap<Novelty, NoveltyDataType>();
            CreateMap<CreateRoomRequestDataType, Novelty>();
            CreateMap<UpdateRoomRequestDataType, Novelty>();

            CreateMap<Person, PersonDataType>();
            CreateMap<CreatePersonRequestDataType, Person>();
            CreateMap<UpdatePersonRequestDataType, Person>();

            CreateMap<Assignment, AssignmentDataType>();

            CreateMap<Notification, NotificationDataType>();

            CreateMap<Event, EventDataType>();
            CreateMap<CreateEventRequestDataType, Event>();
            CreateMap<UpdateEventRequestDataType, Event>();

            CreateMap<Entry, EntryDataType>();
            CreateMap<CreateEntryRequestDataType, Entry>();
        }
    }
}
