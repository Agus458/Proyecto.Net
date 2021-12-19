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
    public class PrecioStore : Store<Precio>, IPrecioStore
    {
        private readonly ApiDbContext Context;

        public PrecioStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public Precio GetActual(Guid ProductId)
        {
            return this.Context.Set<Precio>().Where(Precio => Precio.ProductId == ProductId && Precio.ValidDate >= new DateTime()).OrderBy(Precio => Precio.ValidDate).FirstOrDefault();
        }

        public PaginationDataType<Precio> GetAll(int Skip, int Take, Guid ProductId)
        {
            var Collection = this.Context.Set<Precio>().Include(Precio => Precio.Product).Where(Precio => Precio.ProductId == ProductId);
            return new PaginationDataType<Precio> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public Precio GetById(Guid Id)
        {
            return this.Context.Set<Precio>().Include(Precio => Precio).SingleOrDefault(Entity => Entity.Id == Id);
        }
    }
}
