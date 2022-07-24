using BalanceService.Data;
using Event;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Logging;

namespace BalanceService
{
    public class BillOrderHandler : IHandleMessages<BillOrder>
    {
        private readonly DataContext _dataContext;
        static readonly ILog log = LogManager.GetLogger<BillOrderHandler>();

        public BillOrderHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Handle(BillOrder message, IMessageHandlerContext context)
        {
            log.Info($"Balance has received BillOrder, OrderId={message.OrderId} User name = {message.UserName}");
            var user = await _dataContext.Balance.FirstOrDefaultAsync(balance => balance.UserName == message.UserName);
            bool isAvailable = false;

            if (user != null)
            {
                if ((user.Balance - 200) >= 0)
                {
                    isAvailable = true;
                    user.Balance -= 200;
                    await _dataContext.SaveChangesAsync();
                }
                var orderBilled = new OrderBilled
                {
                    OrderId = message.OrderId,
                    Name = message.UserName,
                    ProductName = message.ProductName,
                    IsAvailable = isAvailable
                };

                await context.Publish(orderBilled);

            }

            log.Info($"Balance Publishing OrderBilled, OrderId={message.OrderId} UserName name = {message.UserName}, ProductName = {message.ProductName}");
            
        }
    }
}
