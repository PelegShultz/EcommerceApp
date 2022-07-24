using StockService.Entities;
using Microsoft.EntityFrameworkCore;

namespace StockService.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<StockEntity> Stock { get; set; }
    }
}
