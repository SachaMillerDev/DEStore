namespace PerformanceReportingService.Models
{
    public class PerformanceReport
    {
        public long Id { get; set; }
        public string StoreName { get; set; }
        public int TotalSales { get; set; }
        public int TotalTransactions { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal NetProfit { get; set; }
    }
}
