using MarketplaceService.Data;
using MarketplaceService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.Extensions
{
    public static class FillDatabaseExtension
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
            MarketplaceEntity Maccabi = new MarketplaceEntity("Maccabi Haifa Kit", "https://tickets.mhaifafc.com/MHaifaFootBallWebLink/1734/get.resource/images/1033/1001464.png?etag=7E029CD54F81DD4CB9A9DA89BD3FC6A9", 200);
            MarketplaceEntity Real = new MarketplaceEntity("Real Madrid Kit", "https://st-adidas-isr.mncdn.com/content/images/thumbs/0109360_real-madrid-2223-home-jersey_ha2654_front-center-view.jpeg", 150);
            MarketplaceEntity Chelsea = new MarketplaceEntity("Chelsea Kit", "https://images.footballfanatics.com/chelsea/chelsea-home-stadium-shirt-2021-22_ss4_p-12056125+u-5mijsfxcr93116g2bhlh+v-7054a0d06c44477aa6f594dd85f81503.jpg?_hv=1&w=900", 100);
            if (!context.Marketplace.Any())
            {
                context.Marketplace.AddRange(
                   Maccabi,
                   Real,
                   Chelsea   
                    );
                context.SaveChanges();
            }
        }
    }
}
