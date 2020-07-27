using Microsoft.EntityFrameworkCore;
using MPIS.User.RepositoryModel.Config;
using System;

namespace MPIS.User.RepositoryModel
{
    public class Context : DbContext
    {
        private const string SchemaCore = "Core";
        private const string SchemaExternal = "Extermal";

        //public DbSet<Notification> Notifications { get; set; }
        public DbSet<User.DomainModel.User> Users { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------------------------------------------------------------------------------------------
            // Core
            // --------------------------------------------------------------------------------------------
            new UserConfiguration(modelBuilder.Entity<User.DomainModel.User>(), Context.SchemaCore);


            // --------------------------------------------------------------------------------------------
            // External
            // --------------------------------------------------------------------------------------------


            // --------------------------------------------------------------------------------------------
            // Data default
            // --------------------------------------------------------------------------------------------

        }
    }
}
