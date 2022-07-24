using Event;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Logging;
using StockService.Data;

namespace StockService
{
    public class CheckAndUpdateStockHandler : IHandleMessages<CheckAndUpdateStock>
    {
        private readonly DataContext _dataContext;
        static readonly ILog log = LogManager.GetLogger<CheckAndUpdateStockHandler>();

        public CheckAndUpdateStockHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Handle(CheckAndUpdateStock message, IMessageHandlerContext context)
        {
            log.Info($"Stock has received CheckAndUpdateStock, Product name = {message.ProductName}");
            var item = await _dataContext.Stock.SingleOrDefaultAsync(stock => stock.ProductName == message.ProductName);
            
            if (item != null)
            {
                if (item.Quantity > 0)
                {
                    item.Quantity--;
                    await _dataContext.SaveChangesAsync();
                }

                var stockAvailable = new StockAvailable
                {
                    OrderId = message.OrderId,
                    ProductName = message.ProductName,
                    IsAvailable = item.Quantity > 0,
                    UserName = message.UserName 
                };

                log.Info($"Stock Publishing StockAvailable,  Product name = {message.ProductName}");

                await context.Publish(stockAvailable);
            }
        }
    }
}
