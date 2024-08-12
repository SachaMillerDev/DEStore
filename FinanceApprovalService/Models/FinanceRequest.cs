namespace FinanceApprovalService.Models
{
    public class FinanceRequest
    {
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public int DurationMonths { get; set; }
    }
}
