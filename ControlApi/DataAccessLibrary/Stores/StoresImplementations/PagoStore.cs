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
        public PaginationDataType<Pago> GetAll(int Skip, int Take, Guid FacturaId)
        {
            var Collection = this.Context.Set<Pago>().Include(Pago => Pago.Factura).Where(Pago => Pago.FacturaId == FacturaId);
            return new PaginationDataType<Pago> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public Precio GetById(Guid Id)
        {
            return this.Context.Set<Precio>().Include(Precio => Precio).SingleOrDefault(Entity => Entity.Id == Id);
        }
    }
}
