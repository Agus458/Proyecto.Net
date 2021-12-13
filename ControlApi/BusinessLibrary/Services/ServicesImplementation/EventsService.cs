using AutoMapper;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes;
using SharedLibrary.DataTypes.Events;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class EventsService : IEventsService
    {
        private readonly IEventsStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        private readonly IStoreByBuilding<Room> RoomStore;

        public EventsService(IEventsStore Store, IMapper Mapper, IHttpContextAccessor Context, IStoreByBuilding<Room> RoomStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.RoomStore = RoomStore;
        }

        public EventDataType Create(CreateEventRequestDataType Data)
        {
            var Room = this.RoomStore.GetById(Data.RoomId);
            if (Room == null) throw new ApiError("Salon Invalido", (int)HttpStatusCode.BadRequest);

            var NewEvent = new Event() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewEvent);

            this.Store.Create(NewEvent);

            return Mapper.Map<EventDataType>(NewEvent);
        }

        public void Delete(Guid Id, Guid RoomId)
        {
            if (Id == Guid.Empty) throw new ApiError("Id Invalido", (int)HttpStatusCode.BadRequest);

            var Door = this.Store.GetById(Id);
            if (Door == null) throw new ApiError("Event Not Found", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Door);
        }

        public IEnumerable<EventDataType> GetAll(Guid RoomId)
        {
            var Room = this.RoomStore.GetById(RoomId);
            if (Room == null) throw new ApiError("Edificio Invalido", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(RoomId);

            return Result.Select(Entity => Mapper.Map<EventDataType>(Entity));
        }

        public EventDataType GetById(Guid Id, Guid RoomId)
        {
            if (Id == Guid.Empty) throw new ApiError("Id Invalido", (int)HttpStatusCode.BadRequest);

            var Entity = this.Store.GetById(Id);
            if (Entity == null) throw new ApiError("Event Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<EventDataType>(Entity);
        }

        public void Update(Guid Id, UpdateEventRequestDataType Data, Guid RoomId)
        {
            if (Id == Guid.Empty) throw new ApiError("Id Invalido", (int)HttpStatusCode.BadRequest);

            var Entity = this.Store.GetById(Id);
            if (Entity == null) throw new ApiError("Event Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Entity);

            this.Store.Update(Entity);
        }
    }
}
