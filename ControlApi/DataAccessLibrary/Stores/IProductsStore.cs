using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores
{
    
    public interface IProductsStore :IStore<Product>
    {
       new PaginationDataType<Product> GetAll(int Skip, int Take);
    }
    
}
