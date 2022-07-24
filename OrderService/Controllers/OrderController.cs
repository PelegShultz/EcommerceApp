using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using OrderService.Data;
using OrderService.Entities;
using System.Dynamic;
using System;
using NServiceBus.Logging;

namespace OrderService.Controllers
{
    public class OrderController : BaseController
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderEntity>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<OrderEntity>>> GetUserOrders(string userName)
        {
            return await _context.Orders.Where(x => x.UserName == userName).ToListAsync();
        }

    }
}
