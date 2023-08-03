using Medical.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medical.DAL
{
    public class CosmosContext : DbContext
    {
        public CosmosContext(DbContextOptions<CosmosContext> options) : base(options)
        {
        }

        public DbSet<Hospital> Hospitals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>().ToContainer("Hospitals").HasPartitionKey(x => x.Id);
        }
    }
}
