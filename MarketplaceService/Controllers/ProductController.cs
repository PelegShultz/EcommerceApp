using MarketplaceService.Data;
using MarketplaceService.DTOs;
using MarketplaceService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceService.Controllers
{
    public class ProductController : BaseController
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketplaceEntity>>> GetProducts()
        {
            return await _context.Marketplace.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarketplaceEntity>> GetProduct(int id)
        {
            return await _context.Marketplace.FindAsync(id);
        }
    }
}
