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
    public class PagoStore : Store<Pago> , IPagoStore
    {
        private readonly ApiDbContext Context;

        public PagoStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }
        public PaginationDataType<Pago> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Collection = this.Context.Set<Pago>().Include(Pago => Pago.Building).Where(Pago => Pago.BuildingId == BuildingId);
            return new PaginationDataType<Pago> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public new Precio GetById(Guid Id)
        {
            return this.Context.Set<Precio>().Include(Precio => Precio.Building).SingleOrDefault(Entity => Entity.Id == Id);
        }
    }
}
