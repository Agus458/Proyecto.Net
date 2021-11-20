using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Pago;
using System;
using SharedLibrary.Error;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLibrary.Services.ServicesImplementation
{
    public class PagoService : IPagoService
    {

        private readonly IPagoStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext context;
        private readonly IBuildingsStore BuildingsStore;

        public PagoService(IPagoStore Store, IMapper Mapper, IHttpContextAccessor Context, IBuildingsStore BuildingsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
        }
        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Pago = this.Store.GetById(Id);
            if (Pago == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);
            this.Store.Delete(Pago);
        }

        public PaginationDataType<PagoDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId);

            return new PaginationDataType<PagoDataType>()
            {
                Collection = Result.Collection.Select(Pago => Mapper.Map<PagoDataType >(Pago))
            };
        }

        public void Update(Guid Id, UpdatePagoRequestDateType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Pago = this.Store.GetById(Id);
            if (Pago == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Pago);
            this.Store.Update(Pago);
        }

        public PagoDataType Create(CreatePagoRequestDataType Data)
        {
            var Building = this.BuildingsStore.GetById(Data.BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var NewPago = new Pago() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewPago);

            this.Store.Create(NewPago);

            return Mapper.Map<PagoDataType>(NewPago);
        }

        public PagoDataType GuiById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Pago = this.Store.GetById(Id);
            if (Pago == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);
            return Mapper.Map<PagoDataType>(Pago);
        }
    }
}
