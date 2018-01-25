using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using tube.Entities;

namespace tube.Data
{
    public class TubeDbContext : DbContext
    {
        public TubeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Apikey> ApiKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted && e.State != EntityState.Unchanged);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as ITracking;
                if (changedOrAddedItem != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            changedOrAddedItem.CreatedDate = DateTime.UtcNow;
                            break;
                        case EntityState.Detached:
                        case EntityState.Modified:
                            changedOrAddedItem.ModifiedDate = DateTime.UtcNow;
                            changedOrAddedItem.Version++;
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TubeDbContext>
    {
        public TubeDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<TubeDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new TubeDbContext(builder.Options);
        }
    }
}
