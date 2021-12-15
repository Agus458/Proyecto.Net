using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores.StoresImplementations
{
    public class ProductStore:Store<Product>, IProductsStore
    {
        private readonly ApiDbContext Context;

        public ProductStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }


       public PaginationDataType<Product> GetAll(int Skip, int Take)
        {
            var Collection = this.Context.Set<Product>().Include(Product => Product.Precios);
            return new PaginationDataType<Product> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }
    }
}
