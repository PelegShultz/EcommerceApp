using MarketplaceService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MarketplaceEntity> Marketplace { get; set; }
    }
}
