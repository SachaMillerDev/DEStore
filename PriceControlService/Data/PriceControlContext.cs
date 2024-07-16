using Microsoft.EntityFrameworkCore;
using PriceControlService.Models;
using System.Collections.Generic;

public class PriceControlContext : DbContext
{
    public PriceControlContext(DbContextOptions<PriceControlContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
}
