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
    public class Store<Target> : IStore<Target> where Target : BaseEntity
    {
        private readonly ApiDbContext Context;

        public Store(ApiDbContext Context)
        {
            this.Context = Context;
        }

        public void Create(Target Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            this.Context.Set<Target>().Add(Entity);

            this.Context.SaveChanges();
        }

        public void Delete(Target Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            this.Context.Set<Target>().Remove(Entity);

            this.Context.SaveChanges();
        }

        public IEnumerable<Target> Get(string[] Relations)
        {
            var RelationsArray = new List<string>() { };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            return this.Context.Set<Target>().GetAllIncluding(RelationsArray.ToArray());
        }

        public PaginationDataType<Target> GetAll(int Skip, int Take, string[] Relations)
        {
            var RelationsArray = new List<string>() { };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            var Collection = this.Context.Set<Target>().GetAllIncluding(RelationsArray.ToArray());
            return new PaginationDataType<Target> { Collection = Collection.Skip(Skip).Take(Take > 0 ? Take : 10).AsEnumerable(), Size = Collection.Count() };
        }

        public Target GetById(Guid Id, string[] Relations)
        {
            var RelationsArray = new List<string>() { };
            if (Relations != null) RelationsArray = RelationsArray.Concat(Relations).ToList();

            return this.Context.Set<Target>().GetAllIncluding(RelationsArray.ToArray()).SingleOrDefault(Entity => Entity.Id == Id);
        }

        public void Update(Target Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            this.Context.Set<Target>().Update(Entity);

            this.Context.SaveChanges();
        }
    }
}
