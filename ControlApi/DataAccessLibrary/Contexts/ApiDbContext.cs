using DataAccessLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly HttpContext HttpContext;
        private readonly ILogger<ApiDbContext> Logger;

        /// <summary>
        /// Consturctor for the Api Context.
        /// </summary>
        /// <param name="Options"></param>
        public ApiDbContext(DbContextOptions Options, IHttpContextAccessor ContextAccessor, ILogger<ApiDbContext> Logger) : base(Options)
        {
            this.HttpContext = ContextAccessor.HttpContext;
            this.Logger = Logger;

            if (this.HttpContext is not null && this.HttpContext.Items.TryGetValue("Tenant", out var Tenant))
            {
                if (Guid.TryParse(Tenant as string, out var Id))
                {
                    this.TenantId = Id;
                }
            }

            this.Filter<MustHaveTenantEntity>(Filter => Filter.Where(ExistingEntity => ExistingEntity.TenantId == this.TenantId));
        }

        /// <summary>
        /// Represents all the Institutions saved in the DataBase.
        /// </summary>
        public DbSet<Tenant> Tenants { get; set; }

        /// <summary>
        /// Represents all the Persons saved in the DataBase.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Represents all the Buildings saved in the DataBase.
        /// </summary>
        public DbSet<Building> Buildings { get; set; }

        /// <summary>
        /// Represents all the Doors saved in the DataBase.
        /// </summary>
        public DbSet<Door> Doors { get; set; }

        /// <summary>
        /// Saves the changes in the context into the database and adds the AddedDate or UpdatedDate if corresponds.
        /// </summary>
       
        public DbSet<Novelty> Novelties { get; set; }

        /// <summary>
        /// Saves the changes in the context into the database and adds the AddedDate or UpdatedDate if corresponds.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
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

            foreach (var Entry in ChangeTracker.Entries<MustHaveTenantEntity>())
            {
                switch (Entry.State)
                {
                    case EntityState.Added:
                        Entry.Entity.Id = Guid.NewGuid();
                        Entry.Entity.TenantId = this.TenantId;
                        Entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        Entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            foreach (var Entry in ChangeTracker.Entries())
            {
                var Data = new {
                    Entry.Entity,
                    User = HttpContext.User.FindFirst(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value,
                    Method = Entry.State
                };
                Logger.LogInformation(JsonConvert.SerializeObject(Data));
            }

            return base.SaveChanges();
        }
    }
}
