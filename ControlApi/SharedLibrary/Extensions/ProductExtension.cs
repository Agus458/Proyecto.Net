using AutoMapper;
using DataAccessLibrary.Entities;
using SharedLibrary.DataTypes.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class ProductExtension
    {

        public static ProductDataType GetDataType(this Product Producto)
        {
            /*
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<Person, PersonDataType>());
            var Mapper = new Mapper(Config);

            return Mapper.Map<PersonDataType>(Person);
            */
            var Config = new MapperConfiguration(Conf => Conf.CreateMap<Product, ProductDataType>());
            var Mapper = new Mapper(Config);

            return Mapper.Map<ProductDataType>(Producto);
        }

        public static void AssignDataType<SDataType>(this Person Person, SDataType Data)
        {
            /*
            var Config = new MapperConfiguration(Conf =>
            {
                Conf.CreateMap<SDataType, Person>().ForAllMembers(Options => Options.Condition((Source, Destination, SrcMember) => SrcMember != null));
            });
            var Mapper = new Mapper(Config);

            Mapper.Map(Data, Person);
            */
        }

    }
}
