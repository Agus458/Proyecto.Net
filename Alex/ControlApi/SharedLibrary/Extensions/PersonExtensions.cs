using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class PersonExtensions
    {
        public static PersonDataType GetDataType(this Person Person)
        {
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<Person, PersonDataType>());
            var Mapper = new Mapper(Config);

            return Mapper.Map<PersonDataType>(Person);
        }

        public static void AssignDataType<SDataType>(this Person Person, SDataType Data)
        {
            var Config = new MapperConfiguration(Conf =>
            {
                Conf.CreateMap<SDataType, Person>().ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null));
            });
            var Mapper = new Mapper(Config);

            Mapper.Map(Data, Person);
        }
    }
}
