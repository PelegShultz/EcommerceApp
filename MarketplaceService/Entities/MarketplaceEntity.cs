using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceService.Entities
{
    public class MarketplaceEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public int Price { get; set; }

        public MarketplaceEntity(string name, string imgUrl, int price)
        {
            Name = name;
            ImgUrl = imgUrl;
            Price = price;
        }

    }
}
