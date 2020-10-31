using Microsoft.EntityFrameworkCore;
using VisitorsInCompany.Models;

namespace VisitorsInCompany.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Visitor> Visitors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite($"DataSource=VisitorsDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            modelBuilder.Entity<Visitor>().ToTable("Visitors");
        }
    }
}
