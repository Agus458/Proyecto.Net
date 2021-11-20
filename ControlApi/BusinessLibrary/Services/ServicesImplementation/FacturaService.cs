using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Factura;
using System;
using SharedLibrary.Error;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
   public class FacturaService : IFacturaService
    {
        private readonly IFacturaStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext context;
        private readonly IBuildingsStore BuildingsStore;

        public FacturaService(IFacturaStore Store, IMapper Mapper, IHttpContextAccessor Context, IBuildingsStore BuildingsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
        }


        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Factura = this.Store.GetById(Id);
            if (Factura == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);
            this.Store.Delete(Factura);
        }

        public PaginationDataType<FacturaDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId);

            return new PaginationDataType<FacturaDataType>()
            {
                Collection = Result.Collection.Select(Factura => Mapper.Map<FacturaDataType>(Factura))
            };
        }

        public FacturaDataType GetBuId(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Factura = this.Store.GetById(Id);
            if (Factura == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);
            return Mapper.Map<FacturaDataType>(Factura);
        }

        public void Update(Guid Id, UpdateFacturaRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Factura = this.Store.GetById(Id);
            if (Factura == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Factura);
            this.Store.Update(Factura);
        }

        public FacturaDataType Create(CreateFacturaRequestDataType Data)
        {
            var Building = this.BuildingsStore.GetById(Data.BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var NewFactura = new Factura() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewFactura);

            this.Store.Create(NewFactura);

            return Mapper.Map<FacturaDataType>(NewFactura);
        }

        
    }
}
