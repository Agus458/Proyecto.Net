using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects.
            CreateMap<User, UserDataType>();
            CreateMap<CreateUserRequestDataType, User>();

        }
    }
}
