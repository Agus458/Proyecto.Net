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
    public class NoveltyStore : Store<Novelty>, INoveltyStore
    {
        private readonly ApiDbContext Context;

        public NoveltyStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public PaginationDataType<Novelty> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Collection = this.Context.Set<Novelty>().Include(Novelty => Novelty.Building).Where(Novelty => Novelty.BuildingId == BuildingId);
            return new PaginationDataType<Novelty> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }
    }
}
