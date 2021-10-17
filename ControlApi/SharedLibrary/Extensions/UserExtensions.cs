using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class UserExtensions
    {
        public static UserDataType GetDataType(this User User)
        {
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<User, UserDataType>());
            var Mapper = new Mapper(Config);

            return Mapper.Map<UserDataType>(User);
        }
    }
}
