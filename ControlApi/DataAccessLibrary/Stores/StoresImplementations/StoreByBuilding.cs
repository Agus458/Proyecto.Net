using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public PaginationDataType<Target> GetAll(int Skip, int Take, Guid BuildingId, string[] Relations)
        {
            var RelationsArray = new List<string>() { "Building" };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            var Collection = this.Context.Set<Target>().GetAllIncluding(RelationsArray.ToArray()).Where(Entity => Entity.BuildingId == BuildingId).OrderByDescending(Entity => Entity.CreatedDate);
            return new PaginationDataType<Target> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public Target GetById(Guid Id, Guid BuildingId, string[] Relations)
        {
            var RelationsArray = new List<string>() { "Building" };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            return this.Context.Set<Target>().GetAllIncluding(RelationsArray.ToArray()).Where(Entity => Entity.BuildingId == BuildingId).SingleOrDefault(Entity => Entity.Id == Id);
        }
    }
}
