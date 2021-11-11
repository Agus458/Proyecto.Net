using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Novelties;
using SharedLibrary.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class NoveltyService : INoveltyService
    {
        private readonly INoveltyStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        private readonly IBuildingsStore BuildingsStore;

        public NoveltyService(INoveltyStore Store, IMapper Mapper, IHttpContextAccessor Context, IBuildingsStore BuildingsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
        }

        public NoveltyDataType Create(CreateNoveltyRequestDataType Data)
        {
            var Building = this.BuildingsStore.GetById(Data.BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var NewNovelty = new Novelty () { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewNovelty);

            this.Store.Create(NewNovelty);

            return Mapper.Map<NoveltyDataType>(NewNovelty);
        }

        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Novelty = this.Store.GetById(Id);
            if (Novelty == null) throw new ApiError("Novelty Not Found", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Novelty);
        }

        public PaginationDataType<NoveltyDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId);

            return new PaginationDataType<NoveltyDataType>();
        }

        public NoveltyDataType GetById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Novelty = this.Store.GetById(Id);
            if (Novelty == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<NoveltyDataType>(Novelty);
        }

        public void Update(Guid Id, UpdateNoveltyRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Novelty = this.Store.GetById(Id);
            if (Novelty == null) throw new ApiError("Novelty Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Novelty);

            this.Store.Update(Novelty);
        }
    }

}
