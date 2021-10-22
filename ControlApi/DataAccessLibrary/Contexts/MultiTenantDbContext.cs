﻿using DataAccessLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace DataAccessLibrary.Contexts
{
    public class MultiTenantDbContext : DbContext
    {
        private readonly Guid TenantId;
        private readonly HttpContext HttpContext;

        public MultiTenantDbContext(DbContextOptions<MultiTenantDbContext> Options, IHttpContextAccessor ContextAccessor) : base(Options)
        {
            this.HttpContext = ContextAccessor.HttpContext;

            if (this.HttpContext is not null && this.HttpContext.Items.TryGetValue("Tenant", out var Aux))
            {
                var Tenant = Aux as Tenant;
                this.TenantId = Tenant.Id;
            }

            this.Filter<BaseEntity>(Filter => Filter.Where(ExistingEntity => ExistingEntity.TenantId == this.TenantId));
        }

        /// <summary>
        /// Represents all the Persons saved in the DataBase.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Represents all the Buildings saved in the DataBase.
        /// </summary>
        public DbSet<Building> Buildings { get; set; }

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
                        Entry.Entity.Id = Guid.NewGuid();
                        Entry.Entity.TenantId = this.TenantId;
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
