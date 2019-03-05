using Causes.UI.Web.Models;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext")
        {
            Database.SetInitializer<AppDbContext>(null);
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

            //var objectContext = (this as IObjectContextAdapter).ObjectContext;
            //objectContext.CommandTimeout = 600;
        }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(28, 28));

            modelBuilder.Entity<TB_USERS>().HasRequired(e => e.TB_ROLES).WithMany(t => t.TB_USERS).HasForeignKey(e => e.ROLE_ID).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TB_ROLES> TB_ROLES { get; set; }
        public DbSet<TB_USERS> TB_USERS { get; set; }
        public DbSet<TB_CAUSES> TB_CAUSES { get; set; }
        public DbSet<TB_SIGNATURES> TB_SIGNATURES { get; set; }
    }
}