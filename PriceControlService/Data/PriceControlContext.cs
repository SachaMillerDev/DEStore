using Microsoft.EntityFrameworkCore;
using PriceControlService.Models;

namespace PriceControlService.Data
{
    public class PriceControlContext : DbContext
    {
        public PriceControlContext(DbContextOptions<PriceControlContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
