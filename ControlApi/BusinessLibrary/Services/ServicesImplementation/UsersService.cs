using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using SharedLibrary.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SharedLibrary.Error;
using System.Net;
using System.Security.Claims;
using DataAccessLibrary.Stores;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using DataAccessLibrary;

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> UserManager;
        private readonly HttpContext Context;
        private readonly IMapper Mapper;
        private readonly ApiDbContext ApiContext;
        private readonly ITenantsStore TenantsStore;

        public UsersService(UserManager<User> UserManager, IHttpContextAccessor Context, IMapper Mapper, ApiDbContext ApiContext, ITenantsStore TenantsStore)
        {
            this.UserManager = UserManager;
            this.Context = Context.HttpContext;
            this.Mapper = Mapper;
            this.ApiContext = ApiContext;
            this.TenantsStore = TenantsStore;
        }

        public async Task<UserDataType> Create(CreateUserRequestDataType Data)
        {
            if (Data == null) throw new ArgumentNullException();

            using (var Transaction = await this.ApiContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var TenantId = this.Context.GetTenant();
                    if (TenantId == Guid.Empty) throw new ApiError("No se ingreso la institucion", (int)HttpStatusCode.BadRequest);

                    if (this.TenantsStore.GetById(TenantId) == null) throw new ApiError("Institucion Invalida", (int)HttpStatusCode.BadRequest);

                    if (Context.User.IsInRole("SuperAdmin") && Data.Role.ToLower() != "admin" || Context.User.IsInRole("Admin") && (Data.Role.ToLower() != "portero" && Data.Role.ToLower() != "gestor"))
                    {
                        throw new ApiError("Invalid user role", (int)HttpStatusCode.BadRequest);
                    }

                    if (await this.UserManager.FindByEmailAsync(Data.Email) != null) throw new ApiError("Email already in use", (int)HttpStatusCode.BadRequest);

                    var NewUser = new User()
                    {
                        UserName = Data.Email,
                        TenantId = TenantId
                    };

                    Mapper.Map(Data, NewUser);

                    var Result = await this.UserManager.CreateAsync(NewUser, Data.Password);
                    if (Result.Succeeded)
                    {
                        Result = await this.UserManager.AddToRoleAsync(NewUser, Data.Role);

                        if (Result.Succeeded)
                        {
                            Transaction.Commit();
                        }
                    }

                    var DataType = Mapper.Map<UserDataType>(NewUser);
                    DataType.Roles = await this.UserManager.GetRolesAsync(NewUser);

                    return DataType;

                }
                catch (Exception)
                {
                    Transaction.Rollback();
                    throw;
                }
            }
        }

        public PaginationDataType<UserDataType> GetAll()
        {
            var Users = this.UserManager.Users.ToList();

            var TenantId = this.Context.GetTenant();
            if (TenantId != Guid.Empty)
            {
                Users = Users.Where(ExistingUser => ExistingUser.TenantId == TenantId).ToList();
            }

            return new PaginationDataType<UserDataType>()
            {
                Collection = Users.Select(User =>
                {
                    var Data = Mapper.Map<UserDataType>(User);

                    Data.Roles = this.UserManager.GetRolesAsync(User).Result;

                    return Data;
                }),
                Size = Users.Count
            };
        }

        public async Task<UserDataType> GetById(string Id)
        {
            var User = await this.UserManager.FindByIdAsync(Id);
            return Mapper.Map<UserDataType>(User);
        }

        public async Task Delete(string Id)
        {
            var User = await this.UserManager.FindByIdAsync(Id);
            if (User == null) throw new ApiError("User not found", (int)HttpStatusCode.NotFound);

            if (this.Context.User.FindFirst(ClaimTypes.Email)?.Value == User.Email) throw new ApiError("You can not delete yourself", (int)HttpStatusCode.BadRequest);

            await this.UserManager.DeleteAsync(User);
        }

        public Task Update(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
