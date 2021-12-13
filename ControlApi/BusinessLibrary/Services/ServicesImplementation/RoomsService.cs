using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Rooms;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class RoomsService : IRoomsService
    {
        private readonly IStoreByBuilding<Room> Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        private readonly IStore<Building> BuildingsStore;

        public RoomsService(IStoreByBuilding<Room> Store, IMapper Mapper, IHttpContextAccessor Context, IStore<Building> BuildingsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
        }

        public RoomDataType Create(CreateRoomRequestDataType Data)
        {
            var Building = this.BuildingsStore.GetById(Data.BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var NewRoom = new Room() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewRoom);

            this.Store.Create(NewRoom);

            return Mapper.Map<RoomDataType>(NewRoom);
        }

        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Room = this.Store.GetById(Id);
            if (Room == null) throw new ApiError("Room Not Found", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Room);
        }

        public PaginationDataType<RoomDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId);

            return new PaginationDataType<RoomDataType>()
            {
                Collection = Result.Collection.Select(Data => Mapper.Map<RoomDataType>(Data)),
                Size = Result.Size
            };
        }

        public RoomDataType GetById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Room = this.Store.GetById(Id);
            if (Room == null) throw new ApiError("Room Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<RoomDataType>(Room);
        }

        public void Update(Guid Id, UpdateRoomRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Room = this.Store.GetById(Id);
            if (Room == null) throw new ApiError("Room Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Room);

            this.Store.Update(Room);
        }
    }

}