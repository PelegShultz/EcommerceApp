using System.ComponentModel.DataAnnotations;

namespace StockService.Entities
{
    public class StockEntity
    {
        public StockEntity(string productName, int quantity)
        {
            ProductName = productName;
            Quantity = quantity;
        }

        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

    }
}
