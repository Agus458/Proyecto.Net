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
    public class InstitutionsService : ITenantsService
    {
        private readonly ITenantsStore Store;

        private readonly UserManager<User> UserManager;

        public InstitutionsService(ITenantsStore Store, UserManager<User> UserManager)
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

                    this.Store.Create(NewTenant);

                    var NewUser = new User() { Email = Data.Email, UserName = Data.Email, TenantId = NewTenant.Id };
                    var Result = await this.UserManager.CreateAsync(NewUser, Data.Password);

                    if (Result.Succeeded)
                    {
                        await this.UserManager.AddToRoleAsync(NewUser, "Admin");
                    }
                }
            }

            return new ApiError("User Email Already in use");
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
    }
}
