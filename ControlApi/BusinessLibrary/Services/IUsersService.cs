using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
    public interface IUsersService
    {
        IEnumerable<UserDataType> GetAll();
        UserDataType GetById(string Id);
        Task Create(CreateUserRequestDataType Data);
    }
}
