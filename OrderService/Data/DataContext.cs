using OrderService.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<OrderEntity> Orders { get; set; }


    }
}
