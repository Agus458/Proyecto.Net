using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores.StoresImplementations
{
    public class NoveltyStore : StoreByBuilding<Novelty>, INoveltyStore
    {
        private readonly ApiDbContext Context;

        public NoveltyStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public PaginationDataType<Novelty> GetByTenant(int Skip, int Take, Guid TenantTd, [Optional] string[] Relations)
        {
            var RelationsArray = new List<string>() { "Building" };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            var Collection = this.Context.Set<Novelty>().GetAllIncluding(RelationsArray.ToArray()).Where(Entity => Entity.Building.TenantId == TenantTd).OrderByDescending(Entity => Entity.CreatedDate);
            return new PaginationDataType<Novelty> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }
    }
}
