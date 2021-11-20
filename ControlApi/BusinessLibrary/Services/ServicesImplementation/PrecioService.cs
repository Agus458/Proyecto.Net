﻿using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Precio;
using System;
using SharedLibrary.Error;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLibrary.Services.ServicesImplementation
{
    public class PrecioService : IPrecioService
    {
        private readonly IPrecioStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext context;
        private readonly IBuildingsStore BuildingsStore;

        public PrecioService(IPrecioStore Store, IMapper Mapper, IHttpContextAccessor Context, IBuildingsStore BuildingsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
        }

      
        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Precio = this.Store.GetById(Id);
            if (Precio == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);
            this.Store.Delete(Precio);
        }

        public PaginationDataType<PrecioDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId);

            return new PaginationDataType<PrecioDataType>()
            {
                Collection = Result.Collection.Select(Precio => Mapper.Map<PrecioDataType>(Precio))
            };
        }

        public PrecioDataType GetBuId(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Precio = this.Store.GetById(Id);
            if (Precio == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);
            return Mapper.Map<PrecioDataType>(Precio);
        }

        public void Update(Guid Id, UpdatePreciosRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Precio = this.Store.GetById(Id);
            if (Precio == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Precio);
            this.Store.Update(Precio);
        }

       public PrecioDataType Create(CreatePrecioRequestDataType Data)
        {
            var Building = this.BuildingsStore.GetById(Data.BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var NewPrecio = new Precio() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewPrecio);

            this.Store.Create(NewPrecio);

            return Mapper.Map<PrecioDataType>(NewPrecio);
        }
    }
}
