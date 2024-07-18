using Microsoft.EntityFrameworkCore;
using LoyaltyCardService.Models;

namespace LoyaltyCardService.Data
{
    public class LoyaltyCardContext : DbContext
    {
        public LoyaltyCardContext(DbContextOptions<LoyaltyCardContext> options)
            : base(options)
        {
        }

        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<LoyaltyCard>().HasData(
                new LoyaltyCard { Id = 1, CustomerName = "John Doe", CardNumber = "1234567890", Points = 100 },
                new LoyaltyCard { Id = 2, CustomerName = "Jane Smith", CardNumber = "0987654321", Points = 200 }
            );
        }
    }
}
