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
    public class FacturaStore : Store<Factura>, IFacturaStore
    {
        private readonly ApiDbContext Context;

        public FacturaStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public PaginationDataType<Factura> GetAll(int Skip, int Take, Guid TenantId)
        {
            var Collection = this.Context.Set<Factura>().Include(Factura => Factura.Tenant).Where(Factura => Factura.TenantId == TenantId);
            return new PaginationDataType<Factura> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public new Factura GetById(Guid Id)
        {
            return this.Context.Set<Factura>().Include(Factura => Factura.TenantId).SingleOrDefault(Entity => Entity.Id == Id);
        }
    }
}
