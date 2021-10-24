using DataAccessLibrary.Contexts;
using DataAccessLibrary.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
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
        private readonly UserManager<User> UserManager;

        public TenantsStore(ApiDbContext Context, UserManager<User> UserManager)
        {
            this.Context = Context;
            this.UserManager = UserManager;
        }

        public Tenant Create(Tenant Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            this.Context.Set<Tenant>().Add(Entity);

            this.Context.SaveChanges();

            return Entity;
        }

        public async Task Delete(Tenant Entity)
        {
            if (Entity == null) throw new ArgumentNullException();

            using (IDbContextTransaction Transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    var User = this.UserManager.Users.FirstOrDefault(ExistingUser => ExistingUser.TenantId == Entity.Id);

                    if (User != null)
                    {
                        var Result = await this.UserManager.DeleteAsync(User);

                        if (Result.Succeeded)
                        {
                            this.Context.Set<Tenant>().Remove(Entity);
                        }

                    }

                    this.Context.SaveChanges();

                    Transaction.Commit();
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                }
            }
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

        public Tenant GetByRut(string Rut)
        {
            return this.Context.Set<Tenant>().SingleOrDefault(Entity => Entity.Rut == Rut);
        }

        public Tenant GetBySocialReason(string SocialReason)
        {
            return this.Context.Set<Tenant>().SingleOrDefault(Entity => Entity.SocialReason == SocialReason);
        }
    }
}
