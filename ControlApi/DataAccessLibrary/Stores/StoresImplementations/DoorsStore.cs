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
    public class DoorsStore : Store<Door>, IDoorsStore
    {
        private readonly ApiDbContext Context;

        public DoorsStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public new PaginationDataType<Door> GetAll(int Skip, int Take)
        {
            var Collection = this.Context.Set<Door>().Include(Door => Door.Building);
            return new PaginationDataType<Door> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public new Door GetById(Guid Id)
        {
            return this.Context.Set<Door>().Include(Door => Door.Building).SingleOrDefault(Entity => Entity.Id == Id);
        }
    }
}
