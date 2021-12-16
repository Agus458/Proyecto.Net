using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Assignment;
using SharedLibrary.DataTypes.Buildings;
using SharedLibrary.DataTypes.Doors;
using SharedLibrary.DataTypes.Entries;
using SharedLibrary.DataTypes.Events;
using SharedLibrary.DataTypes.Factura;
using SharedLibrary.DataTypes.Notifications;
using SharedLibrary.DataTypes.Novelties;
using SharedLibrary.DataTypes.Pago;
using SharedLibrary.DataTypes.Persons;
using SharedLibrary.DataTypes.Precio;
using SharedLibrary.DataTypes.Products;
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
            CreateMap<CreateNoveltyRequestDataType, Novelty>();
            CreateMap<UpdateNoveltyRequestDataType, Novelty>();

            CreateMap<Person, PersonDataType>();
            CreateMap<CreatePersonRequestDataType, Person>();
            CreateMap<UpdatePersonRequestDataType, Person>();

            CreateMap<Assignment, AssignmentDataType>();

            CreateMap<Notification, NotificationDataType>();

            CreateMap<Event, EventDataType>();
            CreateMap<CreateEventRequestDataType, Event>();
            CreateMap<UpdateEventRequestDataType, Event>();

            CreateMap<Room, RoomDataType>();
            CreateMap<CreateRoomRequestDataType, Room>();
            CreateMap<UpdateRoomRequestDataType, Room>();

            CreateMap<Entry, EntryDataType>();
            CreateMap<CreateEntryRequestDataType, Entry>();

            CreateMap<Factura, FacturaDataType>();
            CreateMap<CreateFacturaRequestDataType, Factura>();
            CreateMap<UpdateFacturaRequestDataType, Factura>();


            CreateMap<Product, ProductsDataType>();
            CreateMap<CreateProductsRequestDataType, Product>();
            CreateMap<UpdateProductsRequestDataType, Product>();

            CreateMap<Pago, PagoDataType>();
            CreateMap<CreatePagoRequestDataType, Pago>();
            CreateMap<UpdatePagoRequestDateType, Pago>();

            CreateMap<Precio, PrecioDataType>();
            CreateMap<CreatePrecioRequestDataType, Precio>();
            CreateMap<UpdatePreciosRequestDataType, Precio>();
        }
    }
}
