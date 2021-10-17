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

namespace BusinessLibrary.Services.ServicesImplementation
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> UserManager;
        private readonly ApiDbContext Context;

        public UsersService(UserManager<User> UserManager, ApiDbContext Context)
        {
            this.UserManager = UserManager;
            this.Context = Context;
        }

        public Task Create(CreateUserRequestDataType Data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDataType> GetAll()
        {
            return this.UserManager.Users.ToList().Select(User => User.GetDataType());
        }

        public UserDataType GetById(string Id)
        {
            return this.UserManager.FindByIdAsync(Id).Result.GetDataType();
        }
    }
}
