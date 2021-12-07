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
    public class EventsStore : StoreByBuilding<Event> , IEventsStore
    {
        private readonly ApiDbContext Context;

        public EventsStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public IEnumerable<Event> GetAll(Guid BuildingId)
        {
            return this.Context.Set<Event>().Include(Entity => Entity.Building).Where(Entity => Entity.BuildingId == BuildingId);
        }
    }
}
