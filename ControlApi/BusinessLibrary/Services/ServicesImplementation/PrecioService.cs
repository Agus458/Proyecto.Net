using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using SharedLibrary.Error;
using SharedLibrary.DataTypes.Precio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AutoMapper;
using SharedLibrary.DataTypes;
using Microsoft.AspNetCore.Http;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class PrecioService : IPrecioService
    {
        private readonly IPrecioStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext context;
        private readonly IProductsStore ProductStore;


        public PrecioService(IPrecioStore Store, IMapper Mapper, IHttpContextAccessor Context,IProductsStore ProductStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.context = Context.HttpContext;
            this.ProductStore = ProductStore;
        }

      
        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Precio = this.Store.GetById(Id);
            if (Precio == null) throw new ApiError("Precio Not Found", (int)HttpStatusCode.NotFound);
            this.Store.Delete(Precio);
        }

        public PaginationDataType<PrecioDataType> GetAll(int Skip, int Take,Guid ProductId)
        {

            var product = this.ProductStore.GetById(ProductId);
            if (product == null) throw new ApiError("Prouct Invalid", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take);

            return new PaginationDataType<PrecioDataType>()
            {
                Collection = Result.Collection.Select(Precio => Mapper.Map<PrecioDataType>(Precio)),
                Size = Result.Size
            };
        }

        public PrecioDataType GetBuId(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Precio = this.Store.GetById(Id);
            if (Precio == null) throw new ApiError("Precio Not Found", (int)HttpStatusCode.NotFound);
            return Mapper.Map<PrecioDataType>(Precio);
        }

        public void Update(Guid Id, UpdatePreciosRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Precio = this.Store.GetById(Id);
            if (Precio == null) throw new ApiError("Precio Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Precio);
            this.Store.Update(Precio);
        }

       public PrecioDataType Create(CreatePrecioRequestDataType Data)
        {
            var product = this.ProductStore.GetById(Data.ProductId);
            if (product == null) throw new ApiError("Prouct Invalid", (int)HttpStatusCode.BadRequest);

            var NewPrecio = new Precio() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewPrecio);

            this.Store.Create(NewPrecio);

            return Mapper.Map<PrecioDataType>(NewPrecio);
        }
    }
}
