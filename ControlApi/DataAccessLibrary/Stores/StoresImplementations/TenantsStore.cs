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

        public async Task CreateAsync(Tenant Entity, string Email, string Password)
        {
            if (Entity == null || Email == null || Password == null) throw new ArgumentNullException();

            using (IDbContextTransaction Transaction = this.Context.Database.BeginTransaction())
            {
                try
                {
                    this.Context.Set<Tenant>().Add(Entity);

                    var NewUser = new User() { Email = Email, UserName = Email, TenantId = Entity.Id };
                    var Result = await this.UserManager.CreateAsync(NewUser, Password);

                    if (Result.Succeeded)
                    {
                        await this.UserManager.AddToRoleAsync(NewUser, "Admin");
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
