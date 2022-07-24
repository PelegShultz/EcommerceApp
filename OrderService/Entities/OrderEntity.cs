using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Entities
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int Price { get; set; }
        public string UserName { get; set; }
        public string OrderStatus { get; set; }
        public string ProductName { get; set; }
    }
}
