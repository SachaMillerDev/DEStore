using Microsoft.EntityFrameworkCore;
using LoyaltyCardService.Models;
using System.Collections.Generic;

namespace LoyaltyCardService.Data
{
    public class LoyaltyCardContext : DbContext
    {
        public LoyaltyCardContext(DbContextOptions<LoyaltyCardContext> options)
            : base(options)
        {
        }

        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }
    }
}
