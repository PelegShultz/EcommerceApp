using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Entities;

namespace StockService.Extensions
{
    public class FillDatabaseExtension
    {
        public static void PrepDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                InsertToData(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        public static void InsertToData(DataContext context)
        {
            context.Database.Migrate();
            StockEntity MaccabiStock = new StockEntity("Maccabi Haifa Kit", 20);
            StockEntity RealStock = new StockEntity("Real Madrid Kit", 14);
            StockEntity ChelseaStock = new StockEntity("chelsea Kit", 8);
            if(!context.Stock.Any())
            {
                context.Stock.AddRange(MaccabiStock, RealStock, ChelseaStock);
            }
            context.SaveChanges();

        }
    }
}
