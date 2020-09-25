using System;
using Microsoft.EntityFrameworkCore;

namespace FunctionBrokenSqlDepenency
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        public DbSet<Foobar> Foobars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Foobar>()
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Foobar
    {
        public DateTime MyDate { get; set; }
    }
}
