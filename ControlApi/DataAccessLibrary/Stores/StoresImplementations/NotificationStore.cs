using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores.StoresImplementations
{
    public class NotificationStore : Store<Notification>, INotificationStore
    {
        private readonly ApiDbContext Context;

        public NotificationStore(ApiDbContext Context) : base(Context)
        {
            this.Context = Context;
        }

        public void Clear(string UserId)
        {
            var Rows = this.Context.Set<Notification>().Where(Entity => Entity.UserId == UserId);

            foreach (var Entity in Rows)
            {
                this.Context.Set<Notification>().Remove(Entity);
            }

            this.Context.SaveChanges();
        }

        public PaginationDataType<Notification> GetAll(int Skip, int Take, string UserId, string[] Relations)
        {
            var RelationsArray = new List<string>() { };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            var Collection = this.Context.Set<Notification>().GetAllIncluding(RelationsArray.ToArray()).Where(Entity => Entity.UserId == UserId).OrderByDescending(Entity => Entity.CreatedDate);
            return new PaginationDataType<Notification> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public Notification GetById(Guid Id, string UserId, string[] Relations)
        {
            var RelationsArray = new List<string>() { };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            return this.Context.Set<Notification>().GetAllIncluding(RelationsArray.ToArray()).Where(Entity => Entity.UserId == UserId).SingleOrDefault(Entity => Entity.Id == Id);
        }
    }
}
