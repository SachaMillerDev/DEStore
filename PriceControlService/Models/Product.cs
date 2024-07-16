namespace PriceControlService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SaleType { get; set; } // e.g., "3 for 2", "Buy one get one free"
    }
}
