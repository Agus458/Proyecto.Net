using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Error;
using SharedLibrary.DataTypes.Pago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AutoMapper;
using SharedLibrary.DataTypes;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Extensions;
namespace BusinessLibrary.Services.ServicesImplementation
{
    public class PagoService : IPagoService
    {

        private readonly IPagoStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        private readonly IStore<Factura> FacturaStore;
        public PagoService(IPagoStore Store, IMapper Mapper, IStore<Factura> FacturaStore, IHttpContextAccessor Context)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.FacturaStore = FacturaStore;

        }
        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Pago = this.Store.GetById(Id);
            if (Pago == null) throw new ApiError("Pago Not Found", (int)HttpStatusCode.NotFound);
            this.Store.Delete(Pago);
        }

        public PaginationDataType<PagoDataType> GetAll(int Skip, int Take)
        {


            var Result = this.Store.GetAll(Skip, Take);

            return new PaginationDataType<PagoDataType>()
            {
                Collection = Result.Collection.Select(Pago => Mapper.Map<PagoDataType>(Pago)),
                Size = Result.Size
            };
        }

        public void Update(Guid Id, UpdatePagoRequestDateType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Pago = this.Store.GetById(Id);
            if (Pago == null) throw new ApiError("Pago Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Pago);
            this.Store.Update(Pago);
        }

        public PagoDataType Create(CreatePagoRequestDataType Data)
        {
            var Factura = this.FacturaStore.GetById(Data.FacturaId);
            if (Factura == null) throw new ApiError("Factura Invalida", (int)HttpStatusCode.BadRequest);

            var NewPago = new Pago() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewPago);

            this.Store.Create(NewPago);

            return Mapper.Map<PagoDataType>(NewPago);
        }

        public PagoDataType GutById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Pago = this.Store.GetById(Id);
            if (Pago == null) throw new ApiError("Pago Not Found", (int)HttpStatusCode.NotFound);
            return Mapper.Map<PagoDataType>(Pago);
        }


    }
}
