using BalanceService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BalanceEntity> Balance { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderEntity>().HasKey(au => au.Id);
        //    modelBuilder.Entity<OrderEntity>().Property(au => au.Id).ValueGeneratedOnAdd();
        //}
    }
}
