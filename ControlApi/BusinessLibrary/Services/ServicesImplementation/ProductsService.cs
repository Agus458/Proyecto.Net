using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Error;
using SharedLibrary.DataTypes.Products;
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
    public class ProductsService : IProductsService
    {
        private readonly IProductsStore Store;
        private readonly IMapper Mapper;
        private readonly HttpContext Context;
        private readonly ITenantsStore TenantsStore;
        public ProductsService(IProductsStore Store, IMapper Mapper, ITenantsStore TenantsStore,IHttpContextAccessor Context)
        {
            this.Store = Store;
            this.Mapper = Mapper;
            this.Context = Context.HttpContext;
            this.TenantsStore = TenantsStore;
        }
        
        public ProductsDataType Create(CreateProductsRequestDataType Data)
        {

            var NewProduct = new Product() { Id = Guid.NewGuid() };
            Mapper.Map(Data, NewProduct);

            this.Store.Create(NewProduct);

            return Mapper.Map<ProductsDataType>(NewProduct);

        }

        public void Delete(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Products = this.Store.GetById(Id);
            if (Products == null) throw new ApiError("Product Not Found", (int)HttpStatusCode.NotFound);
            this.Store.Delete(Products);
        }

        public PaginationDataType<ProductsDataType> GetAll(int Skip, int Take)
        {
            var Result = this.Store.GetAll(Skip, Take);

            return new PaginationDataType<ProductsDataType>()
            {
                Collection = Result.Collection.Select(Product =>Mapper.Map<ProductsDataType>(Product)),
                Size = Result.Size
            };
        }

        public ProductsDataType GetById(Guid Id)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);
            var Product = this.Store.GetById(Id);
            if (Product == null) throw new ApiError("Product Not Found", (int)HttpStatusCode.NotFound);
            return Mapper.Map<ProductsDataType>(Product);
        }

        public void Update(Guid Id, UpdateProductsRequestDataType Data)
        {
            if (Id == Guid.Empty) throw new ApiError("Invalido Id", (int)HttpStatusCode.BadRequest);

            var Products = this.Store.GetById(Id);
            if (Products == null) throw new ApiError("Product Not Found", (int)HttpStatusCode.NotFound);

            Mapper.Map(Data, Products);
            this.Store.Update(Products);
        }
    }
}
