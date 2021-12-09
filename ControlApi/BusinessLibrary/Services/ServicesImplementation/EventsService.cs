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
        private readonly IStore<Building> BuildingsStore;

        public EventsService(IEventsStore Store, IMapper Mapper, IHttpContextAccessor Context, IStore<Building> BuildingsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
        }

        public EventDataType Create(CreateEventRequestDataType Data)
        {
            var Building = this.BuildingsStore.GetById(Data.BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalido", (int)HttpStatusCode.BadRequest);

            var NewEvent = new Event() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewEvent);

            this.Store.Create(NewEvent);

            return Mapper.Map<EventDataType>(NewEvent);
        }

        public void Delete(Guid Id, Guid BuildingId)
        {
            if (Id == Guid.Empty) throw new ApiError("Id Invalido", (int)HttpStatusCode.BadRequest);

            var Door = this.Store.GetById(Id, BuildingId);
            if (Door == null) throw new ApiError("Event Not Found", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Door);
        }

        public IEnumerable<EventDataType> GetAll(Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalido", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(BuildingId);

            return Result.Select(Entity => Mapper.Map<EventDataType>(Entity));
        }

        public EventDataType GetById(Guid Id, Guid BuildingId)
        {
            if (Id == Guid.Empty) throw new ApiError("Id Invalido", (int)HttpStatusCode.BadRequest);

            var Entity = this.Store.GetById(Id, BuildingId);
            if (Entity == null) throw new ApiError("Event Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<EventDataType>(Entity);
        }

        public void Update(Guid Id, UpdateEventRequestDataType Data, Guid BuildingId)
        {
            if (Id == Guid.Empty) throw new ApiError("Id Invalido", (int)HttpStatusCode.BadRequest);

            var Entity = this.Store.GetById(Id, BuildingId);
            if (Entity == null) throw new ApiError("Event Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Entity);

            this.Store.Update(Entity);
        }
    }
}
