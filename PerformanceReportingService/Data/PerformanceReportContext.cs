using Microsoft.EntityFrameworkCore;
using PerformanceReportingService.Models;

namespace PerformanceReportingService.Data
{
    public class PerformanceReportContext : DbContext
    {
        public PerformanceReportContext(DbContextOptions<PerformanceReportContext> options)
            : base(options)
        {
        }

        public DbSet<PerformanceReport> PerformanceReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<PerformanceReport>().HasData(
                new PerformanceReport { Id = 1, StoreName = "Store A", TotalSales = 100, TotalTransactions = 50, Revenue = 10000, Expenses = 8000, NetProfit = 2000 },
                new PerformanceReport { Id = 2, StoreName = "Store B", TotalSales = 200, TotalTransactions = 150, Revenue = 20000, Expenses = 16000, NetProfit = 4000 }
            );
        }
    }
}
