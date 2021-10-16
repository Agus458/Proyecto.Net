using DataAccessLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace DataAccessLibrary.Contexts
{
    /// <summary>
    /// The Aplication context that will allow to implement EntityFramework.
    /// </summary>
    public class ApiDbContext : IdentityDbContext<User>
    {
        private readonly Guid TenantId;

        /// <summary>
        /// Consturctor for the Api Context.
        /// </summary>
        /// <param name="Options"></param>
        public ApiDbContext(DbContextOptions Options, IHttpContextAccessor ContextAccessor) : base(Options)
        {
            /*var CurrentInstitution = ContextAccessor.HttpContext?.GetTenant();*/

            this.Filter<BaseEntity>(Filter => Filter.Where(ExistingEntity => ExistingEntity.TenantId == this.TenantId));
        }

        /// <summary>
        /// Represents all the Institutions saved in the DataBase.
        /// </summary>
        public DbSet<Tenant> Tenants { get; set; }

        /// <summary>
        /// Saves the changes in the context into the database and adds the AddedDate or UpdatedDate if corresponds.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            foreach (var Entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (Entry.State)
                {
                    case EntityState.Added:
                        Entry.Entity.TenantId = this.TenantId;
                        Entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        Entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            foreach (var Entry in ChangeTracker.Entries<Tenant>())
            {
                switch (Entry.State)
                {
                    case EntityState.Added:
                        Entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        Entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
