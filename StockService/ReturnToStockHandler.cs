using Event;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Logging;
using StockService.Data;

namespace StockService
{
    public class ReturnToStockHandler : IHandleMessages<ReturnToStock>
    {
        private readonly DataContext _dataContext;
        static readonly ILog log = LogManager.GetLogger<ReturnToStockHandler>();

        public ReturnToStockHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Handle(ReturnToStock message, IMessageHandlerContext context)
        {
            var item = await _dataContext.Stock.SingleOrDefaultAsync(stock => stock.ProductName == message.ProductName);
            
            if(item != null)
            {
                log.Info($"Return item back to stock = {item.ProductName}");
                item.Quantity++;
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
