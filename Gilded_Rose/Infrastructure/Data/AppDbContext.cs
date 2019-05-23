using Gilded_Rose.Core.Models;
using Gilded_Rose.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gilded_Rose.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=items.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* restore me to have    data in db
             modelBuilder.Entity<Player>().HasData(new Player("Sidney", "Crosby", 180, 91, new DateTime(1987, 8, 7)) { Id = 1,Created = DateTime.UtcNow});
             modelBuilder.Entity<Player>().HasData(new Player("Connor", "McDavid", 185, 88, new DateTime(1997, 1, 13)) { Id = 2, Created = DateTime.UtcNow });
             modelBuilder.Entity<Player>().HasData(new Player("Auston", "Matthews", 191, 100, new DateTime(1997, 9, 17)) { Id = 3, Created = DateTime.UtcNow });
             modelBuilder.Entity<Player>().HasData(new Player("Patrick", "Kane", 180, 177, new DateTime(1988, 11, 19)) { Id = 4, Created = DateTime.UtcNow });
             modelBuilder.Entity<Player>().HasData(new Player("Drew", "Doughty", 185, 200, new DateTime(1989, 12, 8)) { Id = 5, Created = DateTime.UtcNow });
             modelBuilder.Entity<Player>().HasData(new Player("Erik", "Karlsson", 183, 190, new DateTime(1990, 5, 31)) { Id = 6, Created = DateTime.UtcNow }); 
             */
        }

        public DbSet<Item> Items { get; set; }

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuditInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseModel)entry.Entity).DateCreated = DateTime.UtcNow;
                }
                ((BaseModel)entry.Entity).DateModified = DateTime.UtcNow;
            }
        }
    }
}

