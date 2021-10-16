﻿using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
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

        public IEnumerable<Target> GetAll()
        {
            return this.Context.Set<Target>().AsEnumerable();
        }

        public Target GetById(Guid Id)
        {
            return this.Context.Set<Target>().SingleOrDefault(Entity => Entity.Id == Id);
        }

        public void Update(Target Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            this.Context.Set<Target>().Update(Entity);

            this.Context.SaveChanges();
        }
    }
}
