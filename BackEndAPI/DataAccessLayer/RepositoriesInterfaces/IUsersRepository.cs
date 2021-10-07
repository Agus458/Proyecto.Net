using DataAccessLayer.Entities;
using Shared.DataTypes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.RepositoriesInterfaces
{
    public interface IUsersRepository
    {
        IEnumerable<UserDataType> GetUsers();

        UserDataType GetUser(Guid Id);

        UserDataType CreateUser(CreateUserDataType Data);

        void UpdateUser(Guid Id, UpdateUserDataType Data);
    }
}
