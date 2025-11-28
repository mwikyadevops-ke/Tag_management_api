using Microsoft.EntityFrameworkCore;
using TagManagement.Models;

namespace TagManagement.Data
{
    public class CaseDBContext : DbContext
    {
        public CaseDBContext(DbContextOptions<CaseDBContext> options)
            : base(options) { }

        public DbSet<Tags> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
