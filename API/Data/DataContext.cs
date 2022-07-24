using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions optins) : base(optins)
        {

        }

        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasKey(au => au.Id);//define key for the table
            modelBuilder.Entity<AppUser>().Property(au => au.Id).ValueGeneratedOnAdd();
        }
    }
}
