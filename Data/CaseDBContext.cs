using Microsoft.EntityFrameworkCore;
using TagManagement.Models;

namespace TagManagement.Data
{
    public class CaseDBContext : DbContext
    {
        public CaseDBContext(DbContextOptions<CaseDBContext> options)
            : base(options) { }

        public DbSet<WeightEvents> WeightEvents { get; set; }
        public DbSet<Tags> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeightEvents>()
                .Property(e => e.AxleWeights)
                .HasColumnType("numeric[]"); // PostgreSQL decimal array

            base.OnModelCreating(modelBuilder);
        }
    }
}
