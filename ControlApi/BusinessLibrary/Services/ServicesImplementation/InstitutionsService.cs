using DataAccessLibrary.Entities;
using DataAccessLibrary.Extensions;
using DataAccessLibrary.Stores;
using Microsoft.AspNetCore.Identity;
using SharedLibrary;
using SharedLibrary.DataTypes.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class InstitutionsService : IInstitutionsService
    {
        private readonly IInstitutionsStore Store;

        private readonly UserManager<User> UserManager;

        public InstitutionsService(IInstitutionsStore Store, UserManager<User> UserManager)
        {
            this.Store = Store;
            this.UserManager = UserManager;
        }

        public async Task<dynamic> Create(CreateInstitutionRequestDataType Data)
        {
            if (Data == null) throw new ArgumentNullException();

            if (await this.UserManager.FindByEmailAsync(Data.Email) == null)
            {
                if (this.Store.GetBySocialReason(Data.SocialReason) == null && this.Store.GetByRut(Data.Rut) == null)
                {
                    var NewInstitution = new Institution() { Id = Guid.NewGuid() };
                    NewInstitution.AssignDataType(Data);

                    this.Store.Create(NewInstitution);

                    var NewUser = new User() { Email = Data.Email, UserName = Data.Email, Institution = NewInstitution };
                    var Result = await this.UserManager.CreateAsync(NewUser, Data.Password);

                    if (Result.Succeeded)
                    {
                        await this.UserManager.AddToRoleAsync(NewUser, "Admin");
                    }
                }
            }

            return new ApiError("User Email Already in use");
        }

        public IEnumerable<InstitutionDataType> GetAll()
        {
            return this.Store.GetAll().Select(Institution => Institution.GetDataType());
        }

        public InstitutionDataType GetById(Guid Id)
        {
            var Institution = this.Store.GetById(Id);

            if (Institution != null) return Institution.GetDataType();

            return null;
        }
    }
}
