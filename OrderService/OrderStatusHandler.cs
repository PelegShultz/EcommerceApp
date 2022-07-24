using Event;
using NServiceBus;
using OrderService.Data;

namespace OrderService
{
    public class OrderStatusHandler : IHandleMessages<GetOrderStatus>
    {
        public readonly DataContext _dataContext;

        public OrderStatusHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Handle(GetOrderStatus message, IMessageHandlerContext context)
        {
            var res = _dataContext.Orders.FirstOrDefault(o => o.OrderId == message.OrderId);
            await context.Reply(new OrderStatus
            {
                OrderId = res.OrderId,
                Status = res.OrderStatus
            }).ConfigureAwait(false);
        }
    }
}
