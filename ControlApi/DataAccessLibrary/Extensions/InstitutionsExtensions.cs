using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Extensions
{
    public static class InstitutionsExtensions
    {
        public static InstitutionDataType GetDataType(this Institution Institution)
        {
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<Institution, InstitutionDataType>());
            var Mapper = new Mapper(Config);

            return Mapper.Map<InstitutionDataType>(Institution);
        }

        public static void AssignDataType(this Institution Institution, CreateInstitutionRequestDataType Data)
        {
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<CreateInstitutionRequestDataType, Institution>());
            var Mapper = new Mapper(Config);

            Mapper.Map(Data,Institution);
        }
    }
}
