namespace LoyaltyCardService.Models
{
    public class LoyaltyCard
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CardNumber { get; set; }
        public int Points { get; set; }
    }
}
