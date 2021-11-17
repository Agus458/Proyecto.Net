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
    public class RoomsStore : Store<Room>, IRoomsStore
    {
        private readonly ApiDbContext Context;

        public RoomsStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }
        public PaginationDataType<Room> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Collection = this.Context.Set<Room>().Include(Novelty => Novelty.Building).Where(Novelty => Novelty.BuildingId == BuildingId);
            return new PaginationDataType<Room> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }
    }
}
