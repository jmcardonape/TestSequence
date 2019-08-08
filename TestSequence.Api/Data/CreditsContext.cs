using Microsoft.EntityFrameworkCore;

namespace TestSequence.Api.Data
{
    public class CreditsContext : DbContext
    {
        public CreditsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CreditsContext).Assembly);
        }
    }
}