using Event;
using Microsoft.EntityFrameworkCore;
using NServiceBus;
using NServiceBus.Logging;
using OrderService.Data;
using OrderService.Entities;

namespace OrderService
{
    public class OrderPolicy : Saga<OrderPolicy.SagaData>,
        IAmStartedByMessages<PlaceOrder>,
        IHandleMessages<OrderBilled>,
        IHandleMessages<StockAvailable>
    {
        static readonly ILog log = LogManager.GetLogger<OrderPolicy>();
        public readonly DataContext _dataContext;

        public OrderPolicy(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Orders Received PlaceOrder, OrderId={message.OrderId} Product name = {message.ProductName}, Product price = {message.Price}");
            if (message != null)
            {
                var orderToDB = new OrderEntity
                {
                    OrderId = message.OrderId,
                    Price = message.Price,
                    ProductName = message.ProductName,
                    UserName = message.UserName,
                    OrderStatus = "Order in Process"
                };

                _dataContext.Orders.Add(orderToDB);
                _dataContext.SaveChanges();

            }

            var checkAndUpdateStock = new CheckAndUpdateStock
            {
                OrderId = message.OrderId,
                UserName = message.UserName,
                ProductName = message.ProductName
            };
            log.Info($"Orders Publishing **CheckStock**, OrderId={message.OrderId} Product name = {message.ProductName}, Product price = {message.UserName}");
            await context.Publish(checkAndUpdateStock);

        }

        public async Task Handle(StockAvailable message, IMessageHandlerContext context)
        {
            log.Info($"Orders Received StockAvailable, OrderId={message.OrderId} , name= {message.UserName}");

            if (!message.IsAvailable)
            {
                log.Info($"Item of orderId: {message.OrderId} out of stock");

                await ChangeOrderStatus("Canceled - Item out of stock", message.OrderId);
                MarkAsComplete();

            }
            else
            {
                var billOrder = new BillOrder
                {
                    OrderId = message.OrderId,
                    UserName = message.UserName,
                    ProductName = message.ProductName
                };

                log.Info($"Order publish BillOrder, OrderId= {billOrder.OrderId} name = {message.UserName}, inStock = {message.IsAvailable}");

                await context.Publish(billOrder);
            }
        }

        public async Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"Order has received orderBilld, Product name = {message.ProductName}, Billed = {message.IsAvailable}");

            if (!message.IsAvailable)
            {
                log.Info($"Not enough balance");
                await ChangeOrderStatus("Canceled - Not enough balance", message.OrderId);

                //return to stock
                var command = new ReturnToStock
                {
                    ProductName = message.ProductName
                };

                await context.Publish(command);
            }
            else
            {
                log.Info($"Enough balance");
                await ChangeOrderStatus("Order Completed", message.OrderId);
            }

            log.Info($"Order Compleated!");
            MarkAsComplete();
        }

        private async Task ChangeOrderStatus(string status, string orderId)
        {
            var orderFromDb = await _dataContext.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            orderFromDb.OrderStatus = status;

            _dataContext.SaveChanges();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.MapSaga(saga => saga.OrderId)
                .ToMessage<PlaceOrder>(msg => msg.OrderId)
                .ToMessage<StockAvailable>(msg => msg.OrderId)
                .ToMessage<OrderBilled>(msg => msg.OrderId);
        }



        //The state of the saga
        public class SagaData : ContainSagaData
        {
            public string OrderId { get; set; }
        }
    }
}
