using AutoMapper;
using DataAccessLayer.Entities;
using Shared.DataTypes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Extensions
{
    public static class UserExtension
    {
        public static UserDataType GetDataType(this User User)
        {
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<User, UserDataType>());
            var Mapper = new Mapper(Config);

            return Mapper.Map<UserDataType>(User);
        }
    }
}
