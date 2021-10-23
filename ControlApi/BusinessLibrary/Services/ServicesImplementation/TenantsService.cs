using DataAccessLibrary.Entities;
using SharedLibrary.Extensions;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Identity;
using SharedLibrary;
using SharedLibrary.DataTypes.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class TenantsService : ITenantsService
    {
        private readonly ITenantsStore Store;

        private readonly UserManager<User> UserManager;

        public TenantsService(ITenantsStore Store, UserManager<User> UserManager)
        {
            this.Store = Store;
            this.UserManager = UserManager;
        }

        public async Task<dynamic> Create(CreateTenantRequestDataType Data)
        {
            if (Data == null) throw new ArgumentNullException();

            if (await this.UserManager.FindByEmailAsync(Data.Email) == null)
            {
                if (this.Store.GetBySocialReason(Data.SocialReason) == null && this.Store.GetByRut(Data.Rut) == null)
                {
                    var NewTenant = new Tenant() { Id = Guid.NewGuid() };
                    NewTenant.AssignDataType(Data);

                    await this.Store.CreateAsync(NewTenant, Data.Email, Data.Password);
                }
            }

            return new ApiError("User Email Already in use");
        }

        public async Task Delete(Guid Id)
        {
            var Tenant = this.Store.GetById(Id);
            if (Tenant != null)
            {
                await this.Store.Delete(Tenant);
            }
        }

        public IEnumerable<TenantDataType> GetAll()
        {
            return this.Store.GetAll().Select(Institution => Institution.GetDataType());
        }

        public TenantDataType GetById(Guid Id)
        {
            var Institution = this.Store.GetById(Id);

            if (Institution != null) return Institution.GetDataType();

            return null;
        }

        public void Update(Guid Id, UpdateTenantRequestDataType Data)
        {
            var Tenant = this.Store.GetById(Id);
            if (Tenant != null)
            {
                Tenant.AssignDataType(Data);
                this.Store.Update(Tenant);
            }
        }
    }
}
