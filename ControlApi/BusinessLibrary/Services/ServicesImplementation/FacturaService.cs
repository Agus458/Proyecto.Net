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
using SharedLibrary.Extensions;

namespace BusinessLibrary.Services.ServicesImplementation
{
   public class FacturaService : IFacturaService
    {
        private readonly IFacturaStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        ///private readonly ITenantsStore TenantsStore;

     

        public FacturaService(IFacturaStore Store, IMapper Mapper, IHttpContextAccessor Context)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            //this.TenantsStore = TenantsStore;
        }


        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Factura = this.Store.GetById(Id);
            if (Factura == null) throw new ApiError("Factura Not Found", (int)HttpStatusCode.NotFound);
            this.Store.Delete(Factura);
        }

        public PaginationDataType<FacturaDataType> GetAll(int Skip, int Take )
        {
         

            var Result = this.Store.GetAll(Skip, Take);

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

     

        public FacturaDataType Create(CreateFacturaRequestDataType Data)
        {
            /*
            var TenantId = this.Context.GetTenant();
            if (TenantId == Guid.Empty) throw new ApiError("No se ingreso la institucion", (int)HttpStatusCode.BadRequest);
            */
            //if (this.TenantsStore.GetById(TenantId) == null) throw new ApiError("Institucion Invalida", (int)HttpStatusCode.BadRequest);

            var NewFactura = new Factura() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewFactura);

            this.Store.Create(NewFactura);

            return Mapper.Map<FacturaDataType>(NewFactura);
        }

        public void Pagar(Guid IdFactura)
        {
            if (IdFactura == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Factura = this.Store.GetById(IdFactura);
            if (Factura == null) throw new ApiError("Factura Not Found", (int)HttpStatusCode.NotFound);
            Factura.Pago = new Pago() { Id = Guid.NewGuid(), Monto=Factura.Monto };
            Factura.Pagada = true;
            this.Store.Update(Factura);

           
            

        }

   
        public void Update(Guid Id, UpdateFacturaRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Factura = this.Store.GetById(Id);
            if (Factura == null) throw new ApiError("Factura Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Factura);
            this.Store.Update(Factura);
        }

    }
}
