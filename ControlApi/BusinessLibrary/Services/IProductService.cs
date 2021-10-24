using System;
using SharedLibrary.DataTypes.Product;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Entities;

namespace BusinessLibrary.Services
{
    public interface IProductService
    {


        //   IEnumerable<ProductDataType> GetAll();

        IEnumerable<ProductDataType> GetAll();

        ProductDataType GetById(string Id);

        ProductDataType GetByName(string nombre);

        Task Create(CreateProductRequestDataType Data);
      
    }
}
