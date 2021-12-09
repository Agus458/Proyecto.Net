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
    public class StoreByBuilding<Target> : Store<Target>, IStoreByBuilding<Target> where Target : MustHaveBuildingEntity
    {
        private readonly ApiDbContext Context;

        public StoreByBuilding(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public PaginationDataType<Target> GetAll(int Skip, int Take, Guid BuildingId)
        {
            var Collection = this.Context.Set<Target>().Include(Entity => Entity.Building).Where(Entity => Entity.BuildingId == BuildingId);
            return new PaginationDataType<Target> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public new Target GetById(Guid Id)
        {
            return this.Context.Set<Target>().Include(Entity => Entity.Building).SingleOrDefault(Entity => Entity.Id == Id);
        }
    }
}
