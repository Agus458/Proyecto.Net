using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Stores.StoresImplementations
{
    public class TenantsStore : ITenantsStore
    {
        private readonly ApiDbContext Context;

        public void Create(Tenant Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            this.Context.Set<Tenant>().Add(Entity);

            this.Context.SaveChanges();
        }

        public void Delete(Tenant Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            this.Context.Set<Tenant>().Remove(Entity);

            this.Context.SaveChanges();
        }

        public IEnumerable<Tenant> GetAll()
        {
            return this.Context.Set<Tenant>().AsEnumerable();
        }

        public Tenant GetById(Guid Id)
        {
            return this.Context.Set<Tenant>().SingleOrDefault(Entity => Entity.Id == Id);
        }

        public void Update(Tenant Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            this.Context.Set<Tenant>().Update(Entity);

            this.Context.SaveChanges();
        }

        public TenantsStore(ApiDbContext Context)
        {
            this.Context = Context;
        }

        public Tenant GetByRut(string Rut)
        {
            var Institution = this.Context.Set<Tenant>().FirstOrDefault(ExistingInstitution => ExistingInstitution.Rut == Rut);

            if (Institution != null) return Institution;

            return null;
        }

        public Tenant GetBySocialReason(string SocialReason)
        {
            var Institution = this.Context.Set<Tenant>().FirstOrDefault(ExistingInstitution => ExistingInstitution.SocialReason == SocialReason);

            if (Institution != null) return Institution;

            return null;
        }
    }
}
