using Medical.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medical.DAL
{
    public class FakeCosmosContext : CosmosContext
    {
        public FakeCosmosContext(DbContextOptions<CosmosContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>().ToContainer("Tests").HasPartitionKey(x => x.Id);
        }
    }
}
