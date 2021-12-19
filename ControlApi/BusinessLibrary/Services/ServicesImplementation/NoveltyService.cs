using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Novelties;
using SharedLibrary.Error;
using SharedLibrary.Helpers;
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
        private readonly IStore<Building> BuildingsStore;
        private readonly IWebHostEnvironment Environment;

        public NoveltyService(INoveltyStore Store, IMapper Mapper, IHttpContextAccessor Context, IStore<Building> BuildingsStore, IWebHostEnvironment Environment)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
            this.Environment = Environment;
        }

        public NoveltyDataType Create(CreateNoveltyRequestDataType Data)
        {
            try
            {
                var Building = this.BuildingsStore.GetById(Data.BuildingId);
                if (Building == null) throw new ApiError("Edificio Invalido", (int)HttpStatusCode.BadRequest);

                Guid Id = Guid.NewGuid();
                string Image = "";
                if (Data.FileImage != null && Data.FileImage.Length > 0 && Data.FileImage.ContentType.Contains("image"))
                {
                    Image = FileHelper.Upload(Data.FileImage, this.Environment, Id.ToString());
                }

                var NewNovelty = new Novelty() { Id = Id, Image = Image };
                Mapper.Map(Data, NewNovelty);

                this.Store.Create(NewNovelty);

                return Mapper.Map<NoveltyDataType>(NewNovelty);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(Guid Id, Guid BuildingId)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Novelty = this.Store.GetById(Id, BuildingId);
            if (Novelty == null) throw new ApiError("Novelty Not Found", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Novelty);
        }

        public PaginationDataType<NoveltyDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId);

            return new PaginationDataType<NoveltyDataType>()
            {
                Collection = Result.Collection.Select(Door => Mapper.Map<NoveltyDataType>(Door)),
                Size = Result.Size
            };
        }

        public NoveltyDataType GetById(Guid Id, Guid BuildingId)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Novelty = this.Store.GetById(Id, BuildingId);
            if (Novelty == null) throw new ApiError("Novelty Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<NoveltyDataType>(Novelty);
        }

        public PaginationDataType<NoveltyDataType> GetByTenant(int Skip, int Take, Guid TenantId)
        {
            var Result = this.Store.GetByTenant(Skip, Take, TenantId);

            return new PaginationDataType<NoveltyDataType>()
            {
                Collection = Result.Collection.Select(Door => Mapper.Map<NoveltyDataType>(Door)),
                Size = Result.Size
            };
        }

        public void Update(Guid Id, UpdateNoveltyRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Novelty = this.Store.GetById(Id);
            if (Novelty == null) throw new ApiError("Novelty Not Found", (int)HttpStatusCode.NotFound);

            if (Data.FileImage != null && Data.FileImage.Length > 0 && Data.FileImage.ContentType == "image/jpeg")
            {
                Novelty.Image = FileHelper.Upload(Data.FileImage, this.Environment, Id.ToString());
            }

            Mapper.Map(Data, Novelty);

            this.Store.Update(Novelty);
        }
    }

}
