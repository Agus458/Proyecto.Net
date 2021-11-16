using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Buildings;
using SharedLibrary.Error;
using SharedLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class BuildingsService : IBuildingsService
    {
        private readonly IStore<Building> Store;
        private readonly IMapper Mapper;
        private readonly ITenantsStore TenantsStore;
        private readonly HttpContext Context;

        public BuildingsService(IStore<Building> Store, IMapper Mapper, ITenantsStore TenantsStore, IHttpContextAccessor Context)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.TenantsStore = TenantsStore;
        }

        public BuildingDataType Create(CreateBuildingRequestDataType Data)
        {
            var TenantId = this.Context.GetTenant();
            if (TenantId == Guid.Empty) throw new ApiError("No se ingreso la institucion", (int)HttpStatusCode.BadRequest);

            if (this.TenantsStore.GetById(TenantId) == null) throw new ApiError("Institucion Invalida", (int)HttpStatusCode.BadRequest);

            var NewBuilding = new Building() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewBuilding);

            this.Store.Create(NewBuilding);

            return Mapper.Map<BuildingDataType>(NewBuilding);
        }

        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Building = this.Store.GetById(Id);
            if (Building == null) throw new ApiError("Building Not Found", (int)HttpStatusCode.NotFound);

            this.Store.Delete(Building);
        }

        public PaginationDataType<BuildingDataType> GetAll(int Skip, int Take)
        {
            var Result = this.Store.GetAll(Skip, Take);

            return new PaginationDataType<BuildingDataType>()
            {
                Collection = Result.Collection.Select(Building => Mapper.Map<BuildingDataType>(Building)),
                Size = Result.Size
            };
        }

        public BuildingDataType GetById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Building = this.Store.GetById(Id);
            if (Building == null) throw new ApiError("Building Not Found", (int)HttpStatusCode.NotFound);

            return Mapper.Map<BuildingDataType>(Building);
        }

        public void Update(Guid Id, UpdateBuildingRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Building = this.Store.GetById(Id);
            if (Building == null) throw new ApiError("Building Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Building);

            this.Store.Update(Building);
        }
    }
}
