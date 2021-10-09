using BusinessLayer.BusinessControllersInterfaces;
using DataAccessLayer.RepositoriesInterfaces;
using Shared.DataTypes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessControllers
{
    public class UsersBusinessController : IUsersBusinessController
    {
        private readonly IUsersRepository Repository;

        public UsersBusinessController(IUsersRepository Repository)
        {
            this.Repository = Repository;
        }

        public UserDataType CreateUser(CreateUserDataType Data)
        {
            return this.Repository.CreateUser(Data);
        }

        public UserDataType GetUser(Guid Id)
        {
            return this.Repository.GetUser(Id);
        }

        public IEnumerable<UserDataType> GetUsers()
        {
            return this.Repository.GetUsers();
        }

        public void UpdateUser(Guid Id, UpdateUserDataType Data)
        {
            this.Repository.UpdateUser(Id, Data);
        }
    }
}
