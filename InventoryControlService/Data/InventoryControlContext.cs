using Microsoft.EntityFrameworkCore;
using InventoryControlService.Models;
using System.Collections.Generic;

namespace InventoryControlService.Data
{
    public class InventoryControlContext : DbContext
    {
        public InventoryControlContext(DbContextOptions<InventoryControlContext> options) : base(options) { }
        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}
