using System;
using System.Collections.Generic;
using DataAccessLibrary;
using SharedLibrary.DataTypes.Products;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Services
{
   public interface IProductsService
    {
        PaginationDataType<ProductsDataType> GetAll(int skip, int Take);

        ProductsDataType GetById(Guid Id);

        ProductsDataType Create(CreateProductsRequestDataType Data);

        void Delete(Guid Id);

        void Update(Guid Id, UpdateProductsRequestDataType Data);

    }
}
