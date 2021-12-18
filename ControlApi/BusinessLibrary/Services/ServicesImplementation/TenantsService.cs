using DataAccessLibrary.Entities;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Error;
using SharedLibrary.DataTypes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AutoMapper;
using SharedLibrary.DataTypes;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class TenantsService : ITenantsService
    {
        private readonly ITenantsStore Store;

        private readonly IProductsStore ProductStore;

        private readonly UserManager<User> UserManager;

        private readonly IMapper Mapper;

        public TenantsService(ITenantsStore Store, UserManager<User> UserManager, IMapper Mapper, IProductsStore ProductStore)
        {
            this.Store = Store;
            this.UserManager = UserManager;
            this.Mapper = Mapper;
            this.ProductStore = ProductStore;
        }

        public TenantDataType Create(CreateTenantRequestDataType Data)
        {
            if (Data == null) throw new ArgumentNullException();

            var Product = this.ProductStore.GetById(Data.ProductId);
            if (Product == null) throw new ApiError("Producto Invalido", (int)HttpStatusCode.BadRequest);

            if (this.Store.GetBySocialReason(Data.SocialReason) == null && this.Store.GetByRut(Data.Rut) == null)
            {
                var NewTenant = new Tenant() { Id = Guid.NewGuid() };
                Mapper.Map(Data, NewTenant);

                var Result = this.Store.Create(NewTenant);

                return Mapper.Map<TenantDataType>(Result);
            }

            throw new ApiError("Tenant SocialReason or Rut in use", (int)HttpStatusCode.BadRequest);
        }

        public async Task Delete(Guid Id)
        {
            var Tenant = this.Store.GetById(Id);
            if (Tenant != null)
            {
                await this.Store.Delete(Tenant);
            }
        }

        public IEnumerable<TenantDataType> Get()
        {
            var Result = this.Store.Get();
            return Result.Select(Tenant => Mapper.Map<TenantDataType>(Tenant));
        }

        public PaginationDataType<TenantDataType> GetAll(int Skip, int Take)
        {
            var Result = this.Store.GetAll(new string[] { "Product" }, Skip, Take);

            return new PaginationDataType<TenantDataType>()
            {
                Collection = Result.Collection.Select(Tenant => Mapper.Map<TenantDataType>(Tenant)),
                Size = Result.Size
            };
        }

        public TenantDataType GetById(Guid Id)
        {
            var Tenant = this.Store.GetById(Id, new string[] { "Product" });

            if (Tenant != null) return Mapper.Map<TenantDataType>(Tenant);

            return null;
        }

        public void Update(Guid Id, UpdateTenantRequestDataType Data)
        {
            var Tenant = this.Store.GetById(Id);
            if (Tenant != null)
            {
                if (Data.ProductId != Guid.Empty)
                {
                    var Product = this.ProductStore.GetById(Data.ProductId);
                    if (Product == null) throw new ApiError("Producto Invalido", (int)HttpStatusCode.BadRequest);
                }

                Mapper.Map(Data, Tenant);
                this.Store.Update(Tenant);
            }
        }
    }
}
