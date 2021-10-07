﻿using Shared.DataTypes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessControllersInterfaces
{
    public interface IUsersBusinessController
    {
        IEnumerable<UserDataType> GetUsers();

        UserDataType GetUser(Guid Id);

        UserDataType CreateUser(CreateUserDataType Data);
    }
}
