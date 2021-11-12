using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DataTypes.Products;
using System;
using SharedLibrary.Error;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext context;
        private readonly IBuildingsStore BuildingsStore;

        public ProductsService(IProductsStore Store, IMapper Mapper,IHttpContextAccessor Context, IBuildingsStore BuildingsStore)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.context = Context.HttpContext;
            this.BuildingsStore = BuildingsStore;
        }
        
        public ProductsDataType Create(CreateProductsRequestDataType Data)
        {
            var Building = this.BuildingsStore.GetById(Data.BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var NewProduct = new Product() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewProduct);

            this.Store.Create(NewProduct);

            return Mapper.Map<ProductsDataType>(NewProduct);

        }

        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Products = this.Store.GetById(Id);
            if (Products == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);
            this.Store.Delete(Products);
        }

        public PaginationDataType<ProductsDataType> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Building = this.BuildingsStore.GetById(BuildingId);
            if (Building == null) throw new ApiError("Edificio Invalida", (int)HttpStatusCode.BadRequest);

            var Result = this.Store.GetAll(Skip, Take, BuildingId);

            return new PaginationDataType<ProductsDataType>()
            {
                Collection = Result.Collection.Select(Product =>Mapper.Map<ProductsDataType>(Product))
            };
        }

        public ProductsDataType GetBuId(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Product = this.Store.GetById(Id);
            if (Product == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);
            return Mapper.Map<ProductsDataType>(Product);
        }

        public void Update(Guid Id, UpdateProductsRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Products = this.Store.GetById(Id);
            if (Products == null) throw new ApiError("Door Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Products);
            this.Store.Update(Products);
        }
    }
}
