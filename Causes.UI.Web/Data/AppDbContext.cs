using Causes.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Causes.UI.Web.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TB_ROLES> TB_ROLES { get; set; }
        public DbSet<TB_USERS> TB_USERS { get; set; }
        public DbSet<TB_CAUSES> TB_CAUSES { get; set; }
        public DbSet<TB_SIGNATURES> TB_SIGNATURES { get; set; }
    }
}