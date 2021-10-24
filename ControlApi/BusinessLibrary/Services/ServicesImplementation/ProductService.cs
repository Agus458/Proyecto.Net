using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using SharedLibrary.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DataTypes.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLibrary.Services.ServicesImplementation
{



    class ProductService : IProductService
    {
        public Task Create(CreateProductRequestDataType Data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDataType> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductDataType GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public ProductDataType GetByName(string nombre)
        {
            throw new NotImplementedException();
        }
    }

}
