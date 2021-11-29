using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Doors;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class DoorsService : IDoorsService
    {
        private readonly IStoreByBuilding<Door> Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        private readonly IStore<Building> BuildingsStore;

        public DoorsService(IStoreByBuilding<Door> Store, IMapper Mapper, IHttpContextAccessor Context, IStore<Building> BuildingsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
        }

        public DoorDataType Create(CreateDoorRequestDataType Data)
        {
            var Building = this.BuildingsStore.GetById(Data.BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var NewDoor = new Door() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewDoor);

            this.Store.Create(NewDoor);

            return Mapper.Map<DoorDataType>(NewDoor);
        }

        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Door = this.Store.GetById(Id);
            if (Door == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Door);
        }

        public PaginationDataType<DoorDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId);

            return new PaginationDataType<DoorDataType>()
            {
                Collection = Result.Collection.Select(Door => Mapper.Map<DoorDataType>(Door)),
                Size = Result.Size
            };
        }

        public DoorDataType GetById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Door = this.Store.GetById(Id);
            if (Door == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<DoorDataType>(Door);
        }

        public void Update(Guid Id, UpdateDoorRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Door = this.Store.GetById(Id);
            if (Door == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Door);

            this.Store.Update(Door);
        }
    }
}
