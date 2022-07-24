using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using MarketplaceService.Data;
using MarketplaceService.Entities;
using System.Dynamic;
using System;
using NServiceBus.Logging;
using MarketplaceService.DTOs;
using Event;

namespace MarketplaceService.Controllers
{
    public class MarketplaceController : BaseController
    {
        private readonly DataContext _context;

        static ILog log = LogManager.GetLogger<MarketplaceController>();//remove that
        private readonly ILogger<MarketplaceController> _log;
        private readonly IMessageSession _messageSession;

        public MarketplaceController(DataContext context, IMessageSession messageSession, ILogger<MarketplaceController> logger)
        {
            _context = context;
            _messageSession = messageSession;
            _log = logger;
        }



        [HttpPost("placeorder")]
        public async Task<OrderStatus> PlaceOrder(ProductDto productDto)
        {
            string orderId = Guid.NewGuid().ToString().Substring(0, 8);
            var product = await _context.Marketplace.FirstOrDefaultAsync(product => product.Name == productDto.Name);
            if (product != null)
            {
                var command = new PlaceOrder
                {
                    OrderId = orderId,
                    ProductName = product.Name,
                    Price = product.Price,
                    ImgUrl = product.ImgUrl,
                    UserName = productDto.UserName,
                };
                log.Info($"user name = {productDto.UserName}, {product.Name}");
                // Send the command
                await _messageSession.Send(command)
                   .ConfigureAwait(false);

                //_log.LogInformation($"Marketplace Sending PlaceOrder,OrderId={command.OrderId} ProductName = {command.ProductName}, ProductPrice = {command.Price}");
                _log.LogInformation("wait 5 sec for order status");
                await Task.Delay(TimeSpan.FromSeconds(5));

                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
                var res = await _messageSession.Request<OrderStatus>(new GetOrderStatus
                {
                    OrderId = orderId 
                }, cancellationTokenSource.Token).ConfigureAwait(false);

                return res;
            }

            return null;

        }

    }
}
